import { Link } from "react-router-dom";
import { useState, useContext, createContext } from "react";
import CurrencyDropdown from "../CurrencyDropdown";
import CurrencyContext from "../CurrencyContext";
function Navbar({ handleCurrencyURL }) {
    const { currency, url } = useContext(CurrencyContext);
    const [loggedCurrency, setLoggedCurrency] = useState(currency);
    const handleCurrency = (value) => {
        setLoggedCurrency(value);
        console.log(value);
        handleCurrencyURL(value);
    }
    return (
        <>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container-fluid">
                    <a className="navbar-brand" href="/">AirFlight</a>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <nav>
                            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                {/*<li className="nav-item">*/}
                                {/*    <Link className="nav-link active" aria-current="page" to={`${url}`}>Home</Link>*/}
                                {/*</li>*/}
                                {/*<li className="nav-item">*/}
                                {/*    <Link className="nav-link" to={`${url}/about-us`} > About</Link>*/}
                                {/*</li>*/}
                                {/*<li className="nav-item">*/}
                                {/*    <Link className="nav-link" to={`${url}/contact-us`}> Contact</Link>*/}
                                {/*</li>*/}
                                {/*<li className="nav-item">*/}
                                {/*    <Link className="nav-link" to={`${url}/managebooking`}> Manage Booking</Link>*/}
                                {/*</li>*/}
                                {/*<li className="nav-item">*/}
                                {/*    <Link className="nav-link" to={`${url}/login`} > Login Account</Link>*/}
                                {/*</li>*/}
                                <li className="nav-item">
                                    <a className="nav-link active" aria-current="page" href={`${url}`}>Home</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href={`${url}/about-us`} > About</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href={`${url}/contact-us`}> Contact</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href={`${url}/managebooking`}> Manage Booking</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href={`${url}/login`} > Login Account</a>
                                </li>
                            </ul >
                        </nav>
                        <a className="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Region
                        </a>
                        <CurrencyDropdown handleCurrency={handleCurrency} />
                    </div >
                </div >
            </nav >
        </>
    );
}
export default Navbar;