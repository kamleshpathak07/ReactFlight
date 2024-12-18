import { useContext } from "react";
import CurrencyContext from './CurrencyContext';
function CurrencyDropdown({ handleCurrency }) {
    const { currency, url } = useContext(CurrencyContext);
    return (
        <ul className="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
            <li><a className="dropdown-item" href="#en-IN" onClick={(event) => handleCurrency('INR')}>India</a></li>
            <li><a className="dropdown-item" href="#en-EU" onClick={(event) => handleCurrency('EUR')}>Ireland</a></li>
            <li><a className="dropdown-item" href="#en-GB" onClick={(event) => handleCurrency('GBP')}>United Kingdom</a></li>
        </ul>
    );
}
export default CurrencyDropdown;