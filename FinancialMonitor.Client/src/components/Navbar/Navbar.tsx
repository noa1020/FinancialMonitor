import { NavLink } from "react-router-dom";
import "./Navbar.css";

export default function Navbar(){

    return(

        <header className="navbar">

            <div className="navbar-container">

                <h2>
                    Financial Monitor
                </h2>

                <nav>

                    <NavLink
                        to="/add"
                        className={({isActive})=>
                            isActive
                            ?"active"
                            :""
                        }
                    >
                        Add Transaction
                    </NavLink>

                    <NavLink
                        to="/monitor"
                        className={({isActive})=>
                            isActive
                            ?"active"
                            :""
                        }
                    >
                        Live Monitor
                    </NavLink>

                </nav>

            </div>

        </header>

    );

}