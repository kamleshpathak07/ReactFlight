import { useState, useMemo } from 'react';
import { useLocation } from 'react-router-dom';
import FlightFilter from './FlightFilter';
import FlightResult from './FlightResult';
function FlightResultPanel() {
    const location = useLocation();
    const { state } = location;
    const result = state.result.data.result.airSolutions ?? [];
    const paxInfo = state.paxInfo ?? {};
    const token = state.result.data.result.token;
    const [flighData, setFlightData] = useState([]);
    const [flightFilterData, setFlightFilterData] = useState([]);
    const [hasResult, setHasResult] = useState(false);
    useMemo(() => {
        setFlightData(result);
        setFlightFilterData(result);
    }, [result]);
    const onFilterHandleData = (airsolutionList) => {
        const result = airsolutionList;
        setFlightData(result.airsolutionList);
    }
    return (
        <>
            <h2>Flight Result Panel</h2>
            <section className="AirFlightResponse">
                <FlightFilter airsoltions={flightFilterData} onFilterHandleData={onFilterHandleData} />
                <FlightResult airsolutions={flighData} token={token} paxInfo={paxInfo} />
            </section>
        </>
    )
}
export default FlightResultPanel;