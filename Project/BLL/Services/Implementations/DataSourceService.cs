using BLL.DTOs;
using BLL.Options;
using BLL.Services.Abstractions;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Options;
using VDS.RDF.Query;

namespace BLL.Services.Implementations
{
    public class DataSourceService : IDataSourceService
    {
        private readonly SourceOptions _sourceOptions;
        private readonly ApplicationDbContext _context;

        public DataSourceService(IOptions<SourceOptions> sourceOptions, ApplicationDbContext context)
        {
            _sourceOptions = sourceOptions.Value;
            _context = context;
        }

        public ScientiestFullModel? GetScientiest(long wikiId)
        {
            var query = $@"
                PREFIX dbo: <http://dbpedia.org/ontology/>
                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
                PREFIX dbp: <http://dbpedia.org/property/>

                SELECT ?abstract ?birthDate ?birthPlace ?occupationLabel ?nativeName ?originalName ?thumbnail
                WHERE {{
                  ?scientist dbo:wikiPageID {wikiId}

                  # Отримуємо анотацію українською, якщо недоступна — англійською
                  OPTIONAL {{
                    ?scientist dbo:abstract ?abstract_uk .
                    FILTER (lang(?abstract_uk) = 'uk')
                  }}
                  OPTIONAL {{
                    ?scientist dbo:abstract ?abstract_en .
                    FILTER (lang(?abstract_en) = 'en')
                  }}
                  BIND (COALESCE(?abstract_uk, ?abstract_en) AS ?abstract)

                  OPTIONAL {{ ?scientist dbo:birthDate ?birthDate . }}
                  OPTIONAL {{ ?scientist dbo:birthPlace ?birthPlace . }}

                  # Отримуємо назву професії українською, якщо недоступна — англійською
                  OPTIONAL {{
                    ?scientist dbo:occupation ?occupation .
                    OPTIONAL {{
                      ?occupation rdfs:label ?occupationLabelUk .
                      FILTER (lang(?occupationLabelUk) = 'uk')
                    }}
                    OPTIONAL {{
                      ?occupation rdfs:label ?occupationLabelEn .
                      FILTER (lang(?occupationLabelEn) = 'en')
                    }}
                    BIND (COALESCE(?occupationLabelUk, ?occupationLabelEn) AS ?occupationLabel)
                  }}

                  OPTIONAL {{ ?scientist dbp:nativeName ?nativeName . }}
                  OPTIONAL {{ ?scientist dbo:thumbnail ?thumbnail . }}
                  OPTIONAL {{ ?scientist dbo:originalName ?originalName . }}
                }}";


            var scientists = new List<ScientiestFullModel>();

            try
            {
                // Create a SPARQL endpoint
                SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri(_sourceOptions.Url));

                // Execute the query and get the results
                SparqlResultSet results = endpoint.QueryWithResultSet(query);

                foreach (var result in results)
                {
                    var scientist = new ScientiestFullModel
                    {
                        Description = result["abstract"].ToString().Split("@").FirstOrDefault() ?? string.Empty,
                        ImageUrl = result["thumbnail"].ToString(),
                        BirthDate = result["birthDate"].ToString().Split("^").FirstOrDefault() ?? string.Empty,
                        Occupation = result["occupationLabel"].ToString().Split("@").FirstOrDefault() ?? string.Empty,
                        BirthPlace = result["birthPlace"].ToString().Split("/").LastOrDefault() ?? string.Empty,
                        BirthPlaceUrl = result["birthPlace"].ToString(),
                        Name = "",
                    };

                    try
                    {
                        scientist.Name = result["nativeName"].ToString().Split("@").FirstOrDefault() ?? string.Empty;
                    }
                    catch
                    {
                        scientist.Name = _context.Scientists.FirstOrDefault(s => s.UniqueWikiId == wikiId)?.Fullname ?? string.Empty;
                        scientist.Name = scientist.Name.Split("@").FirstOrDefault() ?? string.Empty;
                    }

                    scientists.Add(scientist);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return scientists.FirstOrDefault();
        }

        public IEnumerable<Scientist> GetScientists()
        {
            var query = @"
                PREFIX dbo: <http://dbpedia.org/ontology/>
                PREFIX dbp: <http://dbpedia.org/property/>
                PREFIX dbr: <http://dbpedia.org/resource/>
                PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>

                SELECT DISTINCT ?scientist ?scientistLabel ?wikiPageID ?thumbnail
                WHERE {
                  ?scientist dbo:occupation ?occupation ;
                             rdfs:label ?scientistLabel ;
                             dbo:wikiPageID ?wikiPageID .

                  OPTIONAL { ?scientist dbo:thumbnail ?thumbnail . }
                  OPTIONAL { ?scientist dbo:almaMater ?almaMater . }
                  OPTIONAL { ?scientist dbp:almaMater ?almaMater . }

                  ?occupation rdfs:label ?occupationLabel .

                  FILTER (lang(?scientistLabel) = 'uk')
                  FILTER EXISTS {
                    ?almaMater rdfs:label ?almaMaterLabel .
                    FILTER (regex(?almaMaterLabel, ""Шевченка"", ""i"") || regex(?almaMaterLabel, ""Shevchenko"", ""i""))
                  }
                }

            ";

            var scientists = new List<Scientist>();

            try
            {
                // Create a SPARQL endpoint
                SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri(_sourceOptions.Url));

                // Execute the query and get the results
                SparqlResultSet results = endpoint.QueryWithResultSet(query);

                foreach (var result in results)
                {
                    Console.WriteLine($"Scientist: {result["scientist"]}, Label: {result["scientistLabel"]}");

                    var uniqueWikiId = result["wikiPageID"].ToString().Split('^').First();

                    var scientist = new Scientist
                    {
                        Fullname = result["scientistLabel"].ToString(),
                        ImageUrl = result["thumbnail"].ToString(),
                        ResourceUrl = result["scientist"].ToString(),
                        UniqueWikiId = long.Parse(uniqueWikiId.ToString())
                    };

                    scientists.Add(scientist);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return scientists;
        }
    }
}
