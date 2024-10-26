from rdflib import Graph, Namespace, Literal
from rdflib.namespace import XSD, SKOS, RDF

g = Graph()
g.parse('countrues_info.ttl', format='ttl')

NS = Namespace('http://example.com/demo/')
XSD = Namespace('http://www.w3.org/2001/XMLSchema#')
SKOS = Namespace('http://www.w3.org/2004/02/skos/core#')

english_language_code = Literal('eng', datatype=XSD.string)

english_language = None
for lang in g.subjects(NS["iso_639-3_code"], english_language_code):
    english_language = lang
    break

if english_language is None:
    print("Обрана мова не знайдена в графі.")
    exit()

countries = []

for cl in g.subjects(RDF.type, NS.Country_Language):
    lang_value = g.value(cl, NS.language_value)
    if lang_value == english_language:
        country = g.value(cl, NS.spoken_in)
        if country:
            country_name = g.value(country, NS.country_name)
            population = g.value(country, NS.population)
            if country_name and population:
                countries.append({
                    'name': country_name.toPython(),
                    'population': int(population.toPython())
                })

countries_sorted = sorted(countries, key=lambda x: x['population'], reverse=True)

for country in countries_sorted:
    print(f"{country['name']} - {country['population']}")
