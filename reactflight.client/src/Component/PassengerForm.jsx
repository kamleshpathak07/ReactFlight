import { useEffect, useState } from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
function PassengerForm({ paxInfo, onSubmit }) {
    const AdultPax = parseInt(paxInfo.NoOfAdultPax) || 0;
    const YouthPax = parseInt(paxInfo.NoOfYouthPax) || 0;
    const ChildPax = parseInt(paxInfo.NoOfChildPax) || 0;
    const InfantPax = parseInt(paxInfo.NoOfInfantPax) || 0;
    const totalPax = AdultPax + YouthPax + ChildPax + InfantPax;
    const [paxList, setPaxList] = useState([]);
    useEffect(() => {
        setPaxList(Array.from({ length: totalPax }, () => ({
            PaxType: '',
            Title: '',
            FirstName: '',
            LastName: '',
            DateOfBirth: null,
            Gender: '',
            Email: '',
            ContactNo: '',
            FrequentFlyer: ''
        })));
    }, [totalPax]);
    const handleChange = (event, index) => {
        const { name, value } = event.target;
        setPaxList(prevState => {
            const updatedPaxList = [...prevState];
            updatedPaxList[index] = {
                ...updatedPaxList[index], [name]: value
            };
            return updatedPaxList;
        });
    };
    const handleDateofBirth = (index, value, paxtype) => {
        setPaxList(prevState => {
            const updatedPaxList = [...prevState];
            updatedPaxList[index] = { ...updatedPaxList[index], DateOfBirth: value, PaxType: paxtype };
            return updatedPaxList;
        });
    };
    const handleSubmit = (event) => {
        event.preventDefault();
        console.log(paxList);
        onSubmit(paxList);
    };
    const handlePaxType = (index, value) => {
        setPaxList(prevState => {
            const updatedPaxList = [...prevState];
            updatedPaxList[index] = { ...updatedPaxList[index], PaxType: value };
            return updatedPaxList;
        });
    }
    return (
        <>
            <h2>Enter Passenger Details :</h2>
            <form onSubmit={handleSubmit}>
                {[...Array(totalPax)].map((_, index) => {
                    let paxType;
                    if (index < AdultPax) {
                        paxType = 'ADULT';
                    } else if (index < AdultPax + YouthPax) {
                        paxType = 'YOUTH';
                    } else if (index < AdultPax + YouthPax + ChildPax) {
                        paxType = 'CHILD';
                    } else {
                        paxType = 'INFANT';
                    }
                    return (
                        <div key={index}>
                            <h4>Traveller {index + 1}:</h4>
                            <PaxFormRender paxCount={index + 1} paxType={paxType} handleChange={handleChange} handleDateofBirth={handleDateofBirth} dateOfBirth={paxList[index]?.DateOfBirth} handlePaxType={handlePaxType} />
                        </div>
                    );
                })}
                <div className='container'>
                    <div className="row">
                        <div className="mb-2 col">
                            <label htmlFor={`InputEmail-0`} className="form-label">Email address*</label>
                            <input type="email" className="form-control" id={`InputEmail-0`} name='Email' lang='Email' required onChange={(event) => handleChange(event, 0)} />
                        </div>
                        <div className="mb-2 col">
                            <label htmlFor={`InputEmail-0`} className="form-label">Mobile Number*</label>
                            <input type="text" className="form-control" id={`InputMobile-0`} name='ContactNo' lang='ContactNo' required onChange={(event) => handleChange(event, 0)} />
                        </div>
                    </div>
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        </>
    );
}
function PaxFormRender({ paxCount, paxType, handleChange, handleDateofBirth, dateOfBirth, handlePaxType }) {
    const today = new Date();
    const minDate = new Date(today);
    const maxDate = new Date(today);
    if (paxType === 'ADULT') {
        minDate.setFullYear(today.getFullYear() - 130);
        maxDate.setFullYear(today.getFullYear() - 16);
    }
    else if (paxType === 'YOUTH') {
        minDate.setFullYear(today.getFullYear() - 16);
        maxDate.setFullYear(today.getFullYear() - 12);
    }
    else if (paxType === 'CHILD') {
        minDate.setFullYear(today.getFullYear() - 12);
        maxDate.setFullYear(today.getFullYear() - 2);
    }
    else if (paxType == 'INFANT') {
        minDate.setFullYear(today.getFullYear() - 2);
    }
    useEffect(() => {
        handlePaxType(paxCount - 1, paxType);
    }, [paxCount]);
    return (
        <div className='pax-render-cls'>
            <div className='container'>
                <div className="row">
                    <div className='col'>
                        <h6>Passenger Type: {paxType}</h6>
                    </div>
                    <div className="dropdown col">
                        <p>Title:</p>
                        <select className="form-dropdown" name='Title' lang='Title' onChange={(event) => handleChange(event, paxCount - 1)}>
                            <option value="">Select Title</option>
                            <option value="Mr">Mr</option>
                            <option value="Mrs">Mrs</option>
                            <option value="Miss">Miss</option>
                        </select>
                    </div>
                    <div className="mb-3 col">
                        <label htmlFor={`InputFirstName-${paxCount}`} className="form-label">First Name*</label>
                        <input type="text" className="form-control" id={`InputFirstName-${paxCount}`} name='FirstName' lang='FirstName' required onChange={(event) => handleChange(event, paxCount - 1)} />
                    </div>
                    <div className="mb-3 col">
                        <label htmlFor={`InputLastName-${paxCount}`} className="form-label">Last Name*</label>
                        <input type="text" className="form-control" id={`InputLastName-${paxCount}`} name='LastName' lang='LastName' required onChange={(event) => handleChange(event, paxCount - 1)} />
                    </div>
                </div>
                <div className="row">
                    <div className="dropdown col">
                        <p>Gender:</p>
                        <select className="form-dropdown" name='Gender' lang='Gender' onChange={(event) => handleChange(event, paxCount - 1)}>
                            <option value="">Select Gender</option>
                            <option value="M">Male</option>
                            <option value="F">Female</option>
                            <option value="O">Other</option>
                        </select>
                    </div>
                    <div className="mb-3 col">
                        <label htmlFor={`InputDOB-${paxCount}`} className="form-label">Date Of Birth*</label>
                        {paxType === 'ADULT' ? <DatePicker showYearDropdown showMonthDropdown selected={dateOfBirth} maxDate={maxDate} onChange={(date) => { handleDateofBirth(paxCount - 1, date, paxType) }} dropdownMode="select"
                            scrollableYearDropdown yearDropdownItemNumber={100} /> :
                            <DatePicker showYearDropdown showMonthDropdown selected={dateOfBirth} minDate={minDate} maxDate={maxDate} onChange={(date) => { handleDateofBirth(paxCount - 1, date, paxType) }} yearDropdownItemNumber={15} />
                        }
                    </div>
                    <div className="mb-3 col">
                        <label htmlFor={`InputFrequentfl-${paxCount}`} className="form-label">Frequent Flyer Number </label>
                        <input type="text" className="form-control" id={`InputFrequentfl-${paxCount}`} name='FrequentFlyer' lang='FrequentFlyer' onChange={(event) => handleChange(event, paxCount - 1)} />
                    </div>
                </div>
            </div>
        </div>
    );
}
export default PassengerForm;