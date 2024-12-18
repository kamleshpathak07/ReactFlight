import { useMemo, useState, useEffect, useContext } from "react";
import { useNavigate } from 'react-router-dom';
import CurrencyContext from "../CurrencyContext";
function FlightResult({ airsolutions, token, paxInfo }) {
    const [pageIndex, setPageIndex] = useState(0);
    const [prevFlag, setPrevFlag] = useState(false);
    const [nextFlag, setNextFlag] = useState(true);
    const [priceRequest, setPriceRequest] = useState(
        {
            key: '',
            token: '',
            outboundKey: '',
            inboundKey: '',
            optionKeyList: [],
            NoOfInfantPax: 0,
            NoOfAdultPax: 1,
            NoOfChildPax: 0,
            NoOfYouthPax: 0,
            tripType: '',
            Supp: 'GAL'
        }
    );
    const { currency, url } = useContext(CurrencyContext);
    const navigate = useNavigate();
    const result = airsolutions ?? [];
    const solutionCount = result.length;
    let listData = useMemo(() => {
        let TempList = result ?? [];
        return TempList.slice(pageIndex * 10, pageIndex * 10 + 10);
    }, [pageIndex, result]);
    const handleBookFlight = async (event) => {
        var credKey = event.target.lang;
        var keys = credKey.split('^');
        let complexData = {};
        if (keys.length >= 4) {
            if (keys[3] === 'OW') {
                //setPriceRequest(prevpriceRequest => (
                //    {
                //        ...prevpriceRequest,
                //        'token': token,
                //        'key': keys[0],
                //        'outboundKey': keys[1],
                //        'inboundKey': '',
                //        'optionKeyList': [keys[2]],
                //        'tripType': keys[3]
                //    }));
            }
            if (keys[3] === 'RT') {
                //setPriceRequest(prevpriceRequest => (
                //    {
                //        ...prevpriceRequest,
                //        'token': token,
                //        'key': keys[0],
                //        'outboundKey': keys[1],
                //        'inboundKey': keys[4],
                //        'optionKeyList': [keys[2], keys[5]],
                //        'tripType': keys[3]
                //    }));


            }
            const newPriceRequest = {
                ...priceRequest,
                token: token,
                key: keys[0],
                outboundKey: keys[1],
                inboundKey: keys[3] === 'RT' ? keys[4] : '',
                optionKeyList: keys[3] === 'OW' ? [keys[2]] : [keys[2], keys[5]],
                tripType: keys[3]
            };
            try {
                document.getElementsByClassName('lds-spinner')[0].style.display = 'block';
                const result = await fetch("https://localhost:7263/api/Flight/FlightPrice",
                    {
                        method: 'POST',
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify(newPriceRequest)
                    }
                );
                const flightResponse = await result.json();
                const AirsolutionList = flightResponse.data.result.airSolutions;
                complexData = {
                    id: 1,
                    name: 'Sample Item',
                    paxInfo: paxInfo,
                    description: 'This is a sample price Response',
                    airsolution: AirsolutionList
                };
            }
            catch {

                console.error("Error fetching flight data:", error);
            }
            finally {
                document.getElementsByClassName('lds-spinner')[0].style.display = 'none';
            }
            console.log(keys);
        }
        navigate(url + '/flight-price', { state: complexData });
    }
    useEffect(() => {
        setPageIndex(0);
    }, [result]);
    useEffect(() => {
        setNextFlag(!(solutionCount - pageIndex * 10 <= 10));
        setPrevFlag(!(pageIndex === 0));
        return;
    }, [pageIndex, solutionCount]);
    return (
        <div className="flt-result-section">
            <p>Flights Count : {solutionCount}</p>
            <div className="lds-spinner" style={{ display: 'none' }}><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
            <div className="flt-price-loader-sec" style={{ display: "none" }}>
                <span className="price-loader"></span>
            </div>
            {
                listData.length > 0 ? (
                    listData.map((z, index) => (
                        <div className={`airsolution-${index} flt-solution-section`} key={index}>
                            {
                                z.journey.map((journey, jIndex) => (
                                    <div className={`journey-${jIndex} flt-journey-section`} key={jIndex}>
                                        {
                                            journey.optionInfos[0].airSegmentInfos.map((segment, sIndex) => (
                                                <div className={`segment-${sIndex}`} key={sIndex}>
                                                    <div className={`dept-seg-section`}>
                                                        <img src={segment.airlineLogoUrl} alt="Airline Logo" />
                                                        <div className={`seg-det-section`}>
                                                            <span>Origin - </span>
                                                            <p>{`${segment.origin} (${segment.originAirportName ?? ''})`}</p>
                                                            <span>Airline Name - </span>
                                                            <p>{`${segment.airlineName}(${segment.carrier})`}</p>
                                                            <span>Destination - </span>
                                                            <p>{`${segment.destination} (${segment.destinationAirportName ?? ''})`}</p>
                                                            <span>Depart Date - </span>
                                                            <p>{`${segment.departDate} ${segment.departTime}`}</p>
                                                            <span>Arrival Date - </span>
                                                            <p>{`${segment.arrivalDate} ${segment.arrivalTime}`}</p>
                                                            <span>Cabin Class - </span>
                                                            <p>{segment.cabinClass}</p>
                                                            <span>Baggage - </span>
                                                            <p>{segment.baggageInfo.allowance}</p>
                                                            <span>Flight Duration - &nbsp;</span>
                                                            <p>{segment.travelDuration}</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            ))
                                        }
                                    </div>
                                ))

                            }
                            <div className="book-btn-flt">
                                <span>{z.supp}</span>
                                <p>{z.totalPrice} GBP</p>

                                {z.journey.length == 1 ?
                                    <button className="btn btn-success" lang={`${z.key}^${z.journey[0].outBoundKey}^${z.journey[0].optionInfos[0].optionKey}^OW`} onClick={(event) => handleBookFlight(event)}>Book Flight</button>
                                    : <button className="btn btn-success" lang={`${z.key}^${z.journey[0].outBoundKey}^${z.journey[0].optionInfos[0].optionKey}^RT^${z.journey[1].inboundKey}^${z.journey[1].optionInfos[0].optionKey}`} onClick={(event) => handleBookFlight(event)}>Book Flight</button>
                                }
                            </div>
                        </div>
                    ))
                ) : (
                    <p>No flight results available.</p>
                )
            }
            {
                result.length > 0 &&
                <div className="paging-sec-cls">
                    <button type="button" className="btn btn-outline-info" disabled={!prevFlag} onClick={() => setPageIndex(index => index - 1)}>prev</button>
                    <button type="button" className="btn btn-outline-info" disabled={!nextFlag} onClick={() => setPageIndex(index => index + 1)}>next</button>
                </div >
            }

            <div className="lds-spinner" style={{ display: 'none' }}><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
        </div >
    );
}
export default FlightResult;
