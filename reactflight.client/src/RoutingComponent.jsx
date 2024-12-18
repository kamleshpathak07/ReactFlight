import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import FlightSearchPanel from './Component/FlightSearchPanel';
import FlightResultPanel from './Component/FlightResultPanel';
import FlightPrice from './Component/FlightPrice';
import AboutUs from './AboutUs';
import ContactUs from './ContactUs';
import FlightPassenger from './Component/FlightPassenger';
import ManageBookingRef from './Component/ManageBooking/ManageBookingRef';
import ItitneraryDetails from './Component/ItineraryDetails';
import UserLogin from './Component/User/UserLogin';
import { useContext, useState, useEffect } from 'react';
import CurrencyContext from './CurrencyContext';
//import { Navbar } from './Component/Navbar';
const RoutingComponent = () => {
    const { currency, url } = useContext(CurrencyContext);
    const [currencyProvider, setCurrencyprovider] = useState({
        currency: currency ?? '',
        url: url ?? ''
    });
    useEffect(() => {
        setCurrencyprovider({ currency: currency, url: url });
        console.log('Logged Currency ' + currency);
        console.log('Logged Url ' + url);
    }, [currency, url]);
    return (
        <>
            <Router>
                <Routes>
                    <Route path={`${currencyProvider.url}`} element={<FlightSearchPanel />}></Route>
                    <Route path={`${currencyProvider.url}/flight-result`} element={<FlightResultPanel />}></Route>
                    <Route path={`${currencyProvider.url}/flight-price`} element={<FlightPrice />}></Route>
                    <Route path={`${currencyProvider.url}/about-us`} element={<AboutUs />}></Route>
                    <Route path={`${currencyProvider.url}/contact-us`} element={<ContactUs />}></Route>
                    <Route path={`${currencyProvider.url}/passengerdetail`} element={<FlightPassenger />}></Route>
                    <Route path={`${currencyProvider.url}/managebooking`} element={<ManageBookingRef />}></Route>
                    <Route path={`${currencyProvider.url}/itineraryDetails`} element={<ItitneraryDetails />}></Route>
                    <Route path={`${currencyProvider.url}/login`} element={<UserLogin />}></Route>
                    <Route path="*" element={<FlightSearchPanel />}></Route>
                </Routes>
            </Router >
        </>
    );
}
export default RoutingComponent;