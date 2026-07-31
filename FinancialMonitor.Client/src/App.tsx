import {
    BrowserRouter,
    Navigate,
    Route,
    Routes
} from "react-router-dom";

import Navbar from "./components/Navbar/Navbar";
import AddTransaction from "./pages/AddTransaction/AddTransaction";
import Monitor from "./pages/Monitor/Monitor";

export default function App(){

    return(
        <BrowserRouter>
            <Navbar/>
            <Routes>
                <Route
                    path="/"
                    element={<Navigate to="/monitor"/>}
                />
                <Route
                    path="/add"
                    element={<AddTransaction/>}
                />
                <Route
                    path="/monitor"
                    element={<Monitor/>}
                />
            </Routes>
        </BrowserRouter>
    );
}