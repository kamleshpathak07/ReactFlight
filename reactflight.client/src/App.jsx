import Navbar from './Component/Navbar';
import './App.css';
import RoutingComponent from './RoutingComponent';
import { useContext, useState } from 'react';
import CurrencyContext from './CurrencyContext';
function App() {
    const { currency, url } = useContext(CurrencyContext);
    const [currencyProvider, setCurrencyProvider] = useState({
        currency: currency,
        url: url
    });
    const handleCurrencyURL = (value) => {
        if (value === 'GBP') {
            setCurrencyProvider({ currency: 'GBP', url: '/en_gb' });
        }
        else if (value === 'EUR') {
            setCurrencyProvider({ currency: 'EUR', url: '/en_ie' });
        }
        else if (value === 'INR') {
            setCurrencyProvider({ currency: 'INR', url: '/en_in' });
        }
        console.log('Changes in The Context Compnent also Currency ' + value);
    };
    return (
        <CurrencyContext.Provider value={{ ...currencyProvider, setCurrency: handleCurrencyURL }} >
            <div>
                <Navbar handleCurrencyURL={handleCurrencyURL} />
                <RoutingComponent />
            </div>
        </CurrencyContext.Provider >
    );
}
export default App;