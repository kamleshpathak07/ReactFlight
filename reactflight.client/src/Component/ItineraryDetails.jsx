import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
function ItitneraryDetails() {
    const location = useLocation();
    const { state } = location;
    const bookingDetails = state.ItineraryDetailList;
    useEffect(() => {
        console.log(bookingDetails);
    }, []);
    return (
        <div className='itinerary-sec-cls'>
            <h3>Itinerary Details : </h3>
            {(bookingDetails && bookingDetails.paxInfos.length > 0) && < div className='booking-details-cls'>

                <div className='account-cls'>
                    <p><b>Company Details - &nbsp; </b></p>
                    <span>{bookingDetails.accountInfo.companyEmail}, {bookingDetails.accountInfo.companyName}, {bookingDetails.accountInfo.companyCode}</span>
                </div>
                <div className='bok-details seg-det-section'>
                    <span>Booking Reference - </span>
                    <p>{bookingDetails.bookingRef}</p>
                    <span>Product -</span>
                    <p>{bookingDetails.prodRef}</p>
                    <span>Booking Media -</span>
                    <p>{bookingDetails.bookingMedia}</p>
                </div>
                <div className='seg-sec-cls'>
                    <h4>Segment Details - </h4>
                    <div className='seg-det-section'>
                        {
                            bookingDetails.airSegments.map((segment, sIndex) => (
                                <div className={`segment-${sIndex}`} key={sIndex}>
                                    <div className={`mg-dept-seg-section`}>
                                        <img src={segment.airlineLogoUrl} alt="Airline Logo" />
                                        <div className={`seg-det-section`}>
                                            <span>Origin - </span>
                                            <p>{`${segment.origin} `}</p>
                                            <span>Airline Name - </span>
                                            <p>{`${segment.carrier}`}</p>
                                            <span>Destination - </span>
                                            <p>{`${segment.destination}`}</p>
                                            <span>Depart Date - </span>
                                            <p>{`${segment.departDate}`}</p>
                                            <span>Arrival Date - </span>
                                            <p>{`${segment.arrivalDate}`}</p>
                                            <span>Cabin Class - </span>
                                            <p>{segment.class}</p>
                                            <span>Baggage - </span>
                                            <p>{`${segment.baggageDetails}`}</p>
                                        </div>
                                    </div>
                                </div>
                            ))
                        }
                    </div>
                </div>
                <div className='seg-sec-cls'>
                    <h4>Passenger Details - </h4>
                    <div className='seg-det-section'>
                        {
                            bookingDetails.paxInfos.map((pax, sIndex) => (
                                <div className={`segment-${sIndex}`} key={sIndex}>
                                    <div className={`mg-dept-seg-section`}>

                                        <div className={`seg-det-section`}>
                                            <span>Pax Number - </span>
                                            <p>{`${sIndex + 1}`}</p>
                                            <span>Pax Type - </span>
                                            <p>{`${pax.paxType}`}</p>
                                            <span>First Name - </span>
                                            <p>{`${pax.firstName}`}</p>
                                            <span>Last Name - </span>
                                            <p>{`${pax.lastName}`}</p>
                                            <span>Pax DOB - </span>
                                            <p>{`${pax.paxDOB}`}</p>
                                        </div>
                                    </div>
                                </div>
                            ))
                        }
                    </div>
                </div>
                {bookingDetails.pricingInfo !== null &&
                    < div className='seg-sec-cls'>
                        <h4>Fare Details - </h4>
                        <span>Base Price </span>
                        <p>{bookingDetails.pricinginfo.baseprice}</p>
                        <span>Tax</span>
                        <p>{bookingDetails.pricinginfo.tax}</p>
                        <span>Total Price</span>
                        <p>{bookingDetails.pricinginfo.totalprice}</p>
                    </div>
                }
            </div >
            }
        </div>
    )
}

export default ItitneraryDetails;