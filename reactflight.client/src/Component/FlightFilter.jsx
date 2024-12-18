import { useEffect, useMemo, useState } from "react";
import React from "react";
import { Typography } from "@mui/material";
import RangeSlider from 'react-range-slider-input';
import 'react-range-slider-input/dist/style.css';
import { useCallback } from "react";
function FlightFilter({ airsoltions, onFilterHandleData }) {
    const solutionList = airsoltions ?? [];
    const airlineList = {};
    const carrierMap = new Map();
    const [priceRange, setPriceRange] = useState({
        minPrice: 0,
        maxPrice: 100
    });
    const minPrice = Math.floor(Math.min(...solutionList.map(z => z.totalPrice)));
    const maxPrice = Math.ceil(Math.max(...solutionList.map(z => z.totalPrice)));
    const [airlineFilterList, setAirlineFilterList] = useState([]);
    const [stopList, SetStopList] = useState([]);
    const [range, setRange] = useState([minPrice, maxPrice]);
    const [airline, setAirline] = useState([
    ]);
    const handleChangeRange = (newvalue) => {
        setRange(newvalue);
    };
    const handleFilter = (event, value) => {
        let result = [...solutionList];
        if (event.target.lang === 'airline') {
            const updatedAirlineFilterList = event.target.checked
                ? [...airlineFilterList, value]
                : airlineFilterList.filter(z => z !== value);
            setAirlineFilterList(updatedAirlineFilterList);
        }
        if (event.target.lang === 'stop') {
            const updatedStopList = event.target.checked
                ? [...stopList, value]
                : stopList.filter(z => z !== value);
            SetStopList(updatedStopList);
        }
    };
    useMemo(() => {
        const priceRange = {
            minPrice: Math.min(...solutionList.map(z => z.totalPrice)),
            maxPrice: Math.max(...solutionList.map(z => z.totalPrice))
        };
        const rangePriceSlide = [priceRange.minPrice, priceRange.maxPrice];
        setRange(rangePriceSlide);
        solutionList.forEach(solution => {
            solution.journey.forEach(journey => {
                journey.optionInfos.forEach(option => {
                    option.airSegmentInfos.forEach(segment => {
                        const { carrier, airlineLogoUrl } = segment;
                        const minPrice = solution.totalPrice;
                        if (!carrierMap.has(carrier)) {
                            carrierMap.set(carrier, {
                                minPrice: minPrice,
                                airlineLogo: airlineLogoUrl
                            });
                        }
                        else {
                            const existing = carrierMap.get(carrier);
                            if (minPrice < existing.minPrice) {
                                existing.minPrice = minPrice;
                            }
                        }
                    });
                });
            });
        });
        airlineList.airlines = Array.from(carrierMap.entries()).map(([carrier, { minPrice, airlineLogo }]) => ({
            carrier,
            minPrice,
            airlineLogo
        }));
        setAirline(...airline, airlineList.airlines);
        setPriceRange(priceRange);
    }, [airsoltions]);
    useEffect(() => {
        let result = [...solutionList];
        const minPrice = range[0];
        const maxPrice = range[1];
        result = result.filter(z => z.totalPrice >= minPrice && z.totalPrice <= maxPrice);
        const updatedAirlineFilterList = airlineFilterList;
        if (updatedAirlineFilterList.length > 0) {
            result = result.filter(z => z.journey.every(objJourney => objJourney.optionInfos[0].airSegmentInfos.every(segment => updatedAirlineFilterList.includes(segment.carrier))));
        }
        const updatedStopList = stopList
        if (updatedStopList.length > 0) {
            result = result.filter(z => z.journey.every(objJourney => updatedStopList.includes(objJourney.optionInfos[0].airSegmentInfos.length - 1)));
        }
        onFilterHandleData({ airsolutionList: result });
    }, [range, airlineFilterList, stopList]);
    return (
        <div className="Filter-cls-section">
            <div className="price-rng-flt">
                <p>Min Price : <b>{priceRange.minPrice}</b></p>
                <p>Max Price :<b> {priceRange.maxPrice}</b></p>
                <div style={{ width: "32rem", padding: "20px" }}>
                    <Typography gutterBottom>Price Range</Typography>
                    <RangeSlider min={minPrice} max={maxPrice} value={range} onInput={handleChangeRange} />
                    <Typography>
                        Selected Range: {range[0]} - {range[1]}
                    </Typography>
                </div>
            </div>
            <div className="flt-stop-sec airline-cls-flt">
                <h3>Stoppage : </h3>
                <ul>
                    <li>
                        <input type="checkbox" id="stop-0" className="form-check-input" lang="stop" onClick={(event) => handleFilter(event, 0)} />
                        <label htmlFor="stop-0" className="form-check-label">0 Stop</label>
                    </li>
                    <li>
                        <input type="checkbox" id="stop-1" className="form-check-input" lang="stop" onClick={(event) => handleFilter(event, 1)} />
                        <label htmlFor="stop-1" className="form-check-label">1 Stop</label>
                    </li>
                    <li>
                        <input type="checkbox" id="stop-2" className="form-check-input" lang="stop" onClick={(event) => handleFilter(event, 2)} />
                        <label htmlFor="stop-2" className="form-check-label">2 or 2+ Stop</label>
                    </li>
                </ul>
            </div>
            <div className="airline-cls-flt">
                <ul style={{ listStyleType: "none" }}>
                    {
                        airline.map((z, index) => (
                            <li key={index}>
                                <input type="checkbox" className="form-check-input" id={`${z.carrier}`} lang='airline' onClick={(event) => handleFilter(event, z.carrier)} />
                                <p>
                                    {z.carrier}
                                </p>
                                <p>
                                    <img src={z.airlineLogo} alt={`${z.carrier} Airline Logo`} />
                                </p>
                                <p>
                                    {z.minPrice}
                                </p>
                            </li>
                        ))
                    }

                </ul>
            </div>
        </div >
    );
    function ApplyFilter(result) {
        if (airlineFilterList.length > 0) {
            result = result.filter(z => z.journey.every(objJourney => objJourney.optionInfos[0].airSegmentInfos.every(segment => airlineFilterList.includes(segment.carrier))));
        }
        if (baggegeList.length > 0) {
            result = result.filter(z => z.journey.every(objJourney => baggegeList.includes(objJourney.optionInfos[0].airSegmentInfos.length - 1)));
        }
        onFilterHandleData({ airsolutionList: result });
    }
}
export default FlightFilter;