import {ThemeProvider} from "@/components/theme-provider";
import Header from "@/components/header/header.tsx";
import {BrowserRouter} from "react-router-dom";
import {Router} from "@/router/router.tsx";

function App() {
    return (
        <div>
            <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
                <BrowserRouter>
                    <Header/>
                    <Router/>
                </BrowserRouter>
            </ThemeProvider>
        </div>
    );
}

export default App;
