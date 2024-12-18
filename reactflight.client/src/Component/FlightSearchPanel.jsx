import { useEffect, useMemo, useState, useContext } from "react";
import { useNavigate } from 'react-router-dom';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import CurrencyContext from "../CurrencyContext";
function FlightSearchPanel() {
    const today = new Date();
    const sevenDaysFromToday = new Date(today);
    sevenDaysFromToday.setDate(today.getDate() + 7);
    const navigate = useNavigate();
    const [hasMaxPax, setHasMaxPax] = useState(false);
    const [hasResult, setHasResult] = useState(false);
    const [loderFlag, setLoderFlag] = useState(false);
    const [departAirportList, setDepartAirportList] = useState([]);
    const [arrivalAirportList, setArrivalAiportList] = useState([]);
    const [token, SetToken] = useState('');
    const [departureError, setDepartureError] = useState('');
    const [arrivalError, setArrivalError] = useState('');
    const [searchRequest, setSearchRequest] = useState({
        Origin: '',
        Destination: '',
        TripType: 'RT',
        DepartDate: formatDate(today),
        ArrivalDate: formatDate(sevenDaysFromToday),
        Class: 'Economy',
        IsFlexibleDate: false,
        IsDirectFlight: false,
        NoOfInfantPax: 0,
        NoOfAdultPax: 1,
        NoOfChildPax: 0,
        NoOfYouthPax: 0,
        AirlineCode: ''
    });
    const [flighData, setFlightData] = useState([]);
    const [flightFilterData, setFlightFilterData] = useState([]);
    const { currency, url } = useContext(CurrencyContext);
    let complexData = {};
    useMemo(() => {
        setHasMaxPax(searchRequest.NoOfAdultPax + searchRequest.NoOfChildPax + searchRequest.NoOfInfantPax + searchRequest.NoOfYouthPax === 9);
    }, [searchRequest.NoOfAdultPax, searchRequest.NoOfChildPax, searchRequest.NoOfInfantPax, searchRequest.NoOfYouthPax])
    const handleSearch = async () => {
        const airportRegex = /^[A-Za-z\s]+\[[A-Z]{3}\],\s*[A-Za-z\s]+$/;
        if (!searchRequest.Origin) {
            const txt = '<h5> Origin Airport is Empty! </h5>';
            setDepartureError(txt);
            document.getElementById('dept-airport').focus();
            return;
        }
        if (!airportRegex.test(searchRequest.Origin)) {
            const txt = '<h5> Select Airport from dropdown List! </h5>';
            setDepartureError(txt);
            document.getElementById('dept-airport').focus();
            return;
        }
        setDepartureError('');
        if (!searchRequest.Destination) {
            const txt = '<h5> Destination Airport is Empty! </h5>';
            setArrivalError(txt);
            document.getElementById('arr-airport').focus();
            return;
        }
        if (!airportRegex.test(searchRequest.Destination)) {
            const txt = '<h5> Select Airport from dropdown List! </h5>';
            setArrivalError(txt);
            document.getElementById('arr-airport').focus();
            return;
        }
        setArrivalError('');
        setLoderFlag(loader => !loader);
        const request = searchRequest;
        try {
            const result = await fetch("https://localhost:7263/api/Flight/FetchFlight",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': "application/json"
                    },
                    body: JSON.stringify(request)
                }
            );
            const flightResponse = await result.json();
            const AirsolutionList = flightResponse.data.result.airSolutions;
            SetToken(flightResponse.data.result.token);
            setHasResult(result => !result);
            setFlightData(AirsolutionList);
            setFlightFilterData(AirsolutionList);
            complexData = {
                id: 1,
                paxInfo: {
                    NoOfAdultPax: request.NoOfAdultPax,
                    NoOfChildPax: request.NoOfChildPax,
                    NoOfYouthPax: request.NoOfYouthPax,
                    NoOfInfantPax: request.NoOfInfantPax
                },
                description: 'This is a sample Flight Response',
                SearchRequest: request,
                result: flightResponse
            };
            navigate(url + '/flight-result', { state: complexData });
        }
        catch (error) {
            console.error("Error fetching flight data:", error);
            setHasResult(false);
        }
        finally {
            setLoderFlag(loader => !loader);
        }
    }
    const onFilterHandleData = (airsolutionList) => {
        const result = airsolutionList;
        setFlightData(result.airsolutionList);
    }
    const handleInput = async (event) => {
        setDepartureError('');
        setArrivalError('');
        const prefixText = event.target.value;
        const isDeparture = event.target.id === 'dept-airport';
        const isArrival = event.target.id === 'arr-airport';
        setSearchRequest(prevState => ({
            ...prevState,
            [isDeparture ? 'Origin' : 'Destination']: prefixText,
        }));
        if (prefixText.length >= 3) {
            try {
                const response = await fetch("https://localhost:7263/api/Flight/Autocomplete", {
                    method: 'POST',
                    headers: {
                        'Content-Type': "application/json",
                    },
                    body: JSON.stringify({
                        product: 'Flight',
                        prefixtext: prefixText,
                    }),
                });

                const jsonResponse = await response.json();
                if (jsonResponse.success) {
                    const airportList = jsonResponse.data ?? [];
                    if (isDeparture) {
                        setDepartAirportList(airportList);
                    } else if (isArrival) {
                        setArrivalAiportList(airportList);
                    }
                }
            } catch (error) {
                console.error('Error fetching airport data:', error);
            }
        }
        else {
            if (isDeparture) {
                setDepartAirportList([]);
            }
            else if (isArrival) {
                setArrivalAiportList([]);
            }
        }
    };
    const handleInputFocus = (event) => {
        const isDeparture = event.target.id === 'dept-airport';
        const isArrival = event.target.id === 'arr-airport';
        setSearchRequest(prevState => ({
            ...prevState,
            [isDeparture ? 'Origin' : 'Destination']: '',
        }));
        isDeparture ? setArrivalAiportList([]) : setDepartAirportList([]);

    }
    const handleAirportInput = (event, value) => {
        setDepartureError('');
        setArrivalError('');
        if (value === 'departure') {
            setSearchRequest(prevstate => ({ ...prevstate, 'Origin': event.target.innerText }));
        }
        if (value === 'arrival') {
            setSearchRequest(prevstate => ({ ...prevstate, 'Destination': event.target.innerText }));
        }
        setArrivalAiportList([]);
        setDepartAirportList([]);
    }
    const handleDepartDate = (date) => {
        const objdepartDate = date;
        var objreturnDate = new Date(objdepartDate);
        objreturnDate.setDate(objreturnDate.getDate() + 7);
        setSearchRequest(prevSearch => ({ ...prevSearch, 'DepartDate': objdepartDate, 'ArrivalDate': objreturnDate }));
    }
    useEffect(() => {
        const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
        const popoverList = popoverTriggerList.map((popoverTriggerEl) => {
            return new window.bootstrap.Popover(popoverTriggerEl);
        });
        return () => {
            popoverList.forEach((popover) => popover.dispose());
        };
    }, []);
    return (
        <div>
            <h4> Flight Search Panel  </h4>
            <div className="flt-search-panel">
                <div className="form-check form-switch d-flex align-items-center">
                    <p className="mb-0">RT</p>
                    <input
                        className="form-check-input mx-2"
                        type="checkbox"
                        role="switch"
                        id="flexSwitchCheckDefault"
                        onChange={(event) => setSearchRequest(prevState => ({
                            ...prevState,
                            'TripType': event.target.checked ? 'OW' : 'RT'
                        }))}
                    />
                    <p className="mb-0">OW</p>
                </div>
                <div>
                    <label htmlFor="dept-airport" className="form-label" >Departure Airport</label>
                    <input type="text" className="form-control" id="dept-airport" onInput={handleInput} value={searchRequest.Origin} onFocus={handleInputFocus} placeholder="Enter departure Airport..." />
                    <div className="departure-error" dangerouslySetInnerHTML={{ __html: departureError }} />
                    {departAirportList.length > 0 &&
                        <div className="airport-list-cls">
                            <ul style={{ listStyle: "none" }}>
                                {
                                    departAirportList.map((airport, index) => (
                                        <li key={index} onClick={(event) => handleAirportInput(event, 'departure')}> {airport} </li>
                                    ))
                                }
                            </ul>
                        </div>
                    }
                    { }
                </div>
                <div>
                    <label htmlFor="arr-airport" className="form-label">Arrival Airport</label>
                    <input type="text" className="form-control" id="arr-airport" onInput={handleInput} value={searchRequest.Destination} onFocus={handleInputFocus} placeholder="Enter Arrival Airport..." />
                    <div className="arrival-error" dangerouslySetInnerHTML={{ __html: arrivalError }} />
                    {arrivalAirportList.length > 0 &&
                        <div className="airport-list-cls">
                            <ul style={{ listStyle: "none" }}>
                                {
                                    arrivalAirportList.map((airport, index) => (
                                        <li key={index} onClick={(event) => handleAirportInput(event, 'arrival')}> {airport} </li>
                                    ))
                                }
                            </ul>
                        </div>
                    }
                </div>
                <div>
                    <label htmlFor="dept-date" className="form-label">Departure Date</label>
                    <DatePicker selected={searchRequest.DepartDate} minDate={new Date()} onChange={(date) => handleDepartDate(date)} />
                </div>
                {searchRequest.TripType === 'RT' &&
                    <div>
                        <label htmlFor="arr-date" className="form-label">Arrival Date</label>
                        <DatePicker selected={searchRequest.ArrivalDate} minDate={searchRequest.DepartDate} onChange={(date) => setSearchRequest(prevarrival => ({ ...prevarrival, 'ArrivalDate': date }))} />
                    </div>
                }
                <div className="pax-sec-cls">
                    <h5>Passenger :</h5>
                    <div className="pax-cls-inner">
                        <button type="button" className="btn btn-secondary" disabled={searchRequest.NoOfAdultPax === 1} data-bs-container="body" data-bs-trigger={searchRequest.NoOfAdultPax == 1 ? "hover focus" : ""} data-bs-toggle={searchRequest.NoOfAdultPax === 1 ? "popover" : ""} data-bs-placement={searchRequest.NoOfAdultPax === 1 ? "bottom" : ""} data-bs-content={searchRequest.NoOfAdultPax === 1 ? "1 Adult is required!" : ""} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfAdultPax': prevstate.NoOfAdultPax - 1 }))}>-</button>
                        <lable>Adult {searchRequest.NoOfAdultPax}</lable>
                        <button type="button" className="btn btn-secondary" disabled={hasMaxPax} data-bs-container="body" data-bs-toggle={hasMaxPax ? "popover" : ""} data-bs-trigger={hasMaxPax ? "hover focus" : ""} data-bs-placement={hasMaxPax ? "bottom" : ""} data-bs-content={hasMaxPax ? "Max 9 Pax allowed!" : ""} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfAdultPax': prevstate.NoOfAdultPax + 1 }))}>+</button>
                    </div>
                    <div className="pax-cls-inner">
                        <button type="button" className="btn btn-secondary" disabled={searchRequest.NoOfYouthPax === 0} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfYouthPax': prevstate.NoOfYouthPax - 1 }))}>-</button>
                        <lable>Youth {searchRequest.NoOfYouthPax}</lable>
                        <button type="button" className="btn btn-secondary" disabled={hasMaxPax} data-bs-container="body" data-bs-toggle={hasMaxPax ? "popover" : ""} data-bs-placement={hasMaxPax ? "bottom" : ""} data-bs-trigger={hasMaxPax ? "hover focus" : ""} data-bs-content={hasMaxPax ? "Max 9 Pax allowed!" : ""} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfYouthPax': prevstate.NoOfYouthPax + 1 }))}>+</button>
                    </div>
                    <div className="pax-cls-inner">
                        <button type="button" className="btn btn-secondary" disabled={searchRequest.NoOfChildPax === 0} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfChildPax': prevstate.NoOfChildPax - 1 }))}>-</button>
                        <lable>Child {searchRequest.NoOfChildPax}</lable>
                        <button type="button" className="btn btn-secondary" disabled={hasMaxPax} data-bs-container="body" data-bs-toggle={hasMaxPax ? "popover" : ""} data-bs-placement={hasMaxPax ? "bottom" : ""} data-bs-trigger={hasMaxPax ? "hover focus" : ""} data-bs-content={hasMaxPax ? "Max 9 Pax allowed!" : ""} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfChildPax': prevstate.NoOfChildPax + 1 }))}>+</button>
                    </div>
                    <div className="pax-cls-inner">
                        <button type="button" className="btn btn-secondary" disabled={searchRequest.NoOfInfantPax === 0} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfInfantPax': prevstate.NoOfInfantPax - 1 }))}>-</button>
                        <lable>Infant {searchRequest.NoOfInfantPax}</lable>
                        <button type="button" className="btn btn-secondary" disabled={hasMaxPax || searchRequest.NoOfInfantPax === searchRequest.NoOfAdultPax} data-bs-trigger={hasMaxPax ? "hover focus" : ""} onClick={() => setSearchRequest(prevstate => ({ ...prevstate, 'NoOfInfantPax': prevstate.NoOfInfantPax + 1 }))}>+</button>
                    </div>
                </div>
                <div className="cabin-sec-cls">
                    <h3>Cabin Class : </h3>
                    <div className="form-check">
                        <input className="form-check-input" type="radio" value="Economy" name="flexRadioDefault" id="flexRadioDefault1" checked={searchRequest.Class === 'Economy'} onChange={(event) => setSearchRequest(prevstate => ({ ...prevstate, 'Class': event.target.value }))} />
                        <label className="form-check-label" htmlFor="flexRadioDefault1">
                            Economy
                        </label>
                    </div>
                    <div className="form-check">
                        <input className="form-check-input" type="radio" value="Business" name="flexRadioDefault" id="flexRadioDefault2" checked={searchRequest.Class === 'Business'} onChange={(event) => setSearchRequest(prevstate => ({ ...prevstate, 'Class': event.target.value }))} />
                        <label className="form-check-label" htmlFor="flexRadioDefault2">
                            Business
                        </label>
                    </div>
                    <div className="form-check">
                        <input className="form-check-input" type="radio" value="First" name="flexRadioDefault" id="flexRadioDefault3" checked={searchRequest.Class === 'First'} onChange={(event) => setSearchRequest(prevstate => ({ ...prevstate, 'Class': event.target.value }))} />
                        <label className="form-check-label" htmlFor="flexRadioDefault3">
                            First
                        </label>
                    </div>
                </div>
                <div>
                    <button type="button" className="btn btn-primary" onClick={handleSearch}>Search</button>
                </div>
                <section className="AirFlightResponse">
                    <div className="loader" style={{ display: loderFlag ? 'block' : 'none' }}></div>
                </section>
            </div>
        </div>
    );
}
const formatDate = (date) => {
    return date.toISOString().split('T')[0];
};
export default FlightSearchPanel;