import { createContext, useContext } from 'react';
const CurrencyContext = createContext({
    currency: 'GBP',
    url: '/en_gb',
    setCurrency: () => { }
});

export default CurrencyContext;