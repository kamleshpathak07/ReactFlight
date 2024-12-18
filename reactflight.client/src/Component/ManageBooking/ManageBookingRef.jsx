import { useState } from "react";
function ManageBookingRef() {
    const [manageBooking, SetManageBooking] = useState({
        BookingRef: '',
        LastName: ''
    });
    const [isValid, setValid] = useState(false);
    const [dangerText, setDangerText] = useState('');
    const [bookingDetails, setBookingDetails] = useState(null);
    const HandleInput = (event) => {
        setValid(false);
        const id = event.target.id;
        const value = event.target.value;
        SetManageBooking(prevstate => ({
            ...prevstate, [event.target.id]: event.target.value
        }));
    };
    const handleSubmit = async (event) => {
        if (manageBooking.BookingRef.trim() === '') {
            setValid(true);
            setDangerText('Booking Reference Field is Empty');
            return;
        }
        if (manageBooking.LastName.trim() === '') {
            setValid(true);
            setDangerText('Last Name Field is Empty');
            return;
        }
        setValid(false);
        const result = await fetch("https://localhost:7263/api/ManageBooking/ManageBookingRef",
            {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(manageBooking)
            }
        );
        const manageData = await result.json();
        const bookingDetails = manageData.data ?? {};
        setBookingDetails(bookingDetails);
        console.log(bookingDetails);
        console.log(manageBooking);
        console.log(isValid);
    }
    return (
        <>
            <div className='manage-booking-sec'>
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label htmlFor="bokreference" children="form-label">Booking Reference*</label>
                        <input type="text" value={manageBooking.BookingRef} className="form-control" id="BookingRef" placeholder="IBE..." onChange={HandleInput} />

                    </div>
                    <div className="mb-3">
                        <label htmlFor="lastname" className="form-label">Last Name*</label>
                        <input type="text" value={manageBooking.LastName} className="form-control" id="LastName" placeholder="Last Name..." onChange={HandleInput} />
                    </div>
                    <div className="d-grid gap-2">
                        <button type="button" className="btn btn-primary" onClick={handleSubmit}>Button</button>
                    </div>
                </form>
                {isValid && <div className="alert alert-dark" role="alert">
                    {dangerText}
                </div>
                }
            </div>
            {(bookingDetails && bookingDetails.paxInfos.length > 0) && < div className='booking-details-cls'>
                <h3>Booking Details - </h3>
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
            </div >
            }
            {
                (isValid && bookingDetails) && <h3>No Booking Details Found!</h3>
            }
        </>
    )
}
export default ManageBookingRef;