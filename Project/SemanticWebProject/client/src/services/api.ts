const apiUrl = "https://localhost:7011/api";

export const getScientists = async (query: string = '') => {
    const res = await fetch(`${apiUrl}/scientists/get-scientists?query=${query}`);
    return res.json();
}

export const getScientist = async (id?: string) => {
    const res = await fetch(`${apiUrl}/scientists/get-scientist?wikiId=${id}`);
    return res.json();
}