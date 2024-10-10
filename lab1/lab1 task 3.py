from rdflib import Graph, Literal, RDF, URIRef, Namespace
from rdflib.namespace import FOAF, XSD

EX = Namespace("http://example.org/")

g = Graph()

g.bind("ex", EX)
g.bind("foaf", FOAF)

g.add((EX.Keyd, RDF.type, FOAF.Person))
g.add((EX.Keyd, FOAF.name, Literal("Кейд")))
g.add((EX.Keyd, EX.address, Literal("1516 Henry Street, Берклі, Каліфорнія 94709, США")))
g.add((EX.Keyd, EX.degree, Literal("бакалавр біології")))
g.add((EX.Keyd, EX.university, EX.КаліфорнійськийУніверситет))
g.add((EX.Keyd, EX.graduationYear, Literal(2011, datatype=XSD.gYear)))
g.add((EX.Keyd, EX.interests, Literal("птахи, екологія, довкілля, фотографія, подорожі")))
g.add((EX.Keyd, EX.visited, EX.Канада))
g.add((EX.Keyd, EX.visited, EX.Франція))

g.add((EX.Emma, RDF.type, FOAF.Person))
g.add((EX.Emma, FOAF.name, Literal("Емма")))
g.add((EX.Emma, EX.address, Literal("Carrer de la Guardia Civil 20, 46020 Valencia, Spain")))
g.add((EX.Emma, EX.degree, Literal("магістр хімії")))
g.add((EX.Emma, EX.university, EX.УніверситетВаленсії))
g.add((EX.Emma, EX.graduationYear, Literal(2015, datatype=XSD.gYear)))
g.add((EX.Emma, EX.knowledgeArea, Literal("управління відходами, токсичні відходи, забруднення повітря")))
g.add((EX.Emma, EX.interests, Literal("їзда на велосипеді, музика, подорожі")))
g.add((EX.Emma, EX.visited, EX.Португалія))
g.add((EX.Emma, EX.visited, EX.Італія))
g.add((EX.Emma, EX.visited, EX.Франція))
g.add((EX.Emma, EX.visited, EX.Німеччина))
g.add((EX.Emma, EX.visited, EX.Данія))
g.add((EX.Emma, EX.visited, EX.Швеція))

g.add((EX.Keyd, EX.knows, EX.Emma))
g.add((EX.Keyd, EX.metIn, EX.Париж_2014))
g.add((EX.Париж_2014, EX.date, Literal("2014-08", datatype=XSD.gYearMonth)))

g.add((EX.Keyd, EX.visited, EX.Німеччина))

g.add((EX.Emma, EX.age, Literal(36, datatype=XSD.integer)))

g.serialize(destination='graph.ttl', format='turtle')

print("Усі трійки графу:")
for subj, pred, obj in g:
    print(f"{subj} {pred} {obj}")

print("\nТрійки про Емму:")
for subj, pred, obj in g.triples((EX.Emma, None, None)):
    print(f"{subj} {pred} {obj}")

print("\nТрійки, що містять імена людей:")
for subj, pred, obj in g.triples((None, FOAF.name, None)):
    print(f"{subj} {pred} {obj}")
