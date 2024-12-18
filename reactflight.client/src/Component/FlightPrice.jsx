import { useState, useRef, useContext } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import PassengerForm from './PassengerForm';
import CurrencyContext from '../CurrencyContext';
function FlightPrice() {
    const location = useLocation();
    const { state } = location;
    const result = state.airsolution.slice(0, 1) ?? [];
    const paxInfo = state.paxInfo ?? {};
    const navigate = useNavigate();
    const passengerRef = useRef(null);
    const { currency, url } = useContext(CurrencyContext);
    const handleScroll = (event) => {
        event.preventDefault();
        passengerRef.current.scrollIntoView({ behavior: 'smooth', block: 'start' });
    };
    const handlePrice = (event) => {
        const complexData = {
            Id: 1,
            paxInfo: paxInfo
        }
        const key = event.target.lang;
        navigate('/passengerdetail', { state: complexData });
    }
    const handleSubmitForm = async (paxInfoList) => {
        const priceRequest = {
            PaxInfoList: paxInfoList,
            AirSolution: result
        }
        try {
            const result = await fetch("https://localhost:7263/api/Flight/FlightPnr",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(priceRequest)
                }
            );
            const flightResponse = await result.json();
            console.log(flightResponse);
            const complexData = {
                paxInfo: paxInfo,
                ItineraryDetailList: flightResponse.itineraryDetail
            }
            navigate(url + '/itineraryDetails', { state: complexData });
        }
        catch (exception) {
            console.log(exception);
            console.log('Price Response getting error');
        }
    }
    return (
        <div className="flt-price-sec">
            <h1>Flight Price Result </h1>
            <div className="flt-result-section">
                {
                    result.length > 0 ? (
                        result.map((z, index) => (
                            <div className={`airsolution-${index} flt-solution-section`} key={index}>
                                {
                                    z.journey.map((journey, jIndex) => (
                                        <div className={`journey-${jIndex} flt-journey-section`} key={jIndex}>
                                            {
                                                journey.airSegments.map((segment, sIndex) => (
                                                    <div className={`segment-${sIndex}`} key={sIndex}>
                                                        <div className={`dept-seg-section`}>
                                                            <img src={segment.airlineLogoUrl} alt="Airline Logo" />
                                                            <div className={`seg-det-section`}>
                                                                <span>Origin - </span>
                                                                <p>{segment.origin} {segment.airport[0].airportName}</p>
                                                                <span>Airline Name - </span>
                                                                <p>{segment.airlineName}</p>
                                                                <span>Destination - </span>
                                                                <p>{segment.destination} {segment.airport[1].airportName}</p>
                                                                <span>Depart Date - </span>
                                                                <p>{segment.departDatetime}</p>
                                                                <span>Arrival Date - </span>
                                                                <p>{segment.arrivalDatetime}</p>
                                                                <span>Baggage - </span>
                                                                <p>{segment.baggageInfo.allowance}</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                ))
                                            }
                                        </div>
                                    ))
                                }
                                <div className="price-btn-flt">
                                    <p>{z.totalPrice} GBP</p>
                                </div>
                                <div className="book-btn-flt">
                                    {/* <button className="btn btn-primary" lang={`${z.key}`} onClick={(event) => handlePrice(event)}>Book Now</button>*/}
                                    <a href="#passengerComponent" onClick={handleScroll}>Book Now</a>
                                </div>
                            </div>



                        ))
                    ) : (
                        <p>No flight results available.</p>
                    )
                }
            </div>
            <div ref={passengerRef} className="passengerComponent" id="passengerComponent">
                <PassengerForm paxInfo={paxInfo} onSubmit={handleSubmitForm} />
            </div>
        </div>
    );
}

export default FlightPrice;