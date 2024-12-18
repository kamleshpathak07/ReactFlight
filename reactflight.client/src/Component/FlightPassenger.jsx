import { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
function FlightPassenger() {
    const location = useLocation();
    const { state } = location;
    const paxInfo = state.paxInfo ?? {};
    const AdultPax = parseInt(paxInfo.NoOfAdultPax) || 0;
    const YouthPax = parseInt(paxInfo.NoOfYouthPax) || 0;
    const ChildPax = parseInt(paxInfo.NoOfChildPax) || 0;
    const InfantPax = parseInt(paxInfo.NoOfInfantPax) || 0;
    const totalPax = AdultPax + YouthPax + ChildPax + InfantPax;
    const [paxList, setPaxList] = useState([{
        PaxType: '',
        Title: '',
        FirstName: '',
        LastName: '',
        DateOfBirth: '',
        Gender: ''
    }]);
    //useEffect(() => {
    //    const newPaxList = Array.from({ totalPax }, () => ({
    //        PaxType: '',
    //        Title: '',
    //        FirstName: '',
    //        LastName: '',
    //        DateOfBirth: '',
    //        Gender: ''
    //    }));

    //    setPaxList(newPaxList);
    //    console.log(paxInfo);
    //    console.log(totalPax);
    //}, []);
    const handleChange = (event, index) => {
        const newvalue = event.target.value;
        //setPaxList(prevState => {
        //    const updatedPaxList = [...prevState];
        //    updatedPaxList[index] = { ...updatedPaxList[index], PaxType: paxtype };
        //    return updatedPaxList;
        //});
        if (event.target.lang === 'title') {
            setPaxList(prevState => {
                const updatedPaxList = [...prevState];
                updatedPaxList[index] = { ...updatedPaxList[index], Title: newvalue };
                return updatedPaxList;
            });
        }
        if (event.target.lang === 'fname') {
            setPaxList(prevState => {
                const updatedPaxList = [...prevState];
                updatedPaxList[index] = { ...updatedPaxList[index], FirstName: newvalue };
                return updatedPaxList;
            });
        }
        if (event.target.lang === 'lname') {
            setPaxList(prevState => {
                const updatedPaxList = [...prevState];
                updatedPaxList[index] = { ...updatedPaxList[index], LastName: newvalue };
                return updatedPaxList;
            });
        }
        if (event.target.lang === 'gender') {
            setPaxList(prevState => {
                const updatedPaxList = [...prevState];
                updatedPaxList[index] = { ...updatedPaxList[index], Gender: newvalue };
                return updatedPaxList;
            });
        }
    };
    return (
        <>
            <h2>Enter Passenger Details :</h2>
            <form>
                {[...Array(totalPax)].map((_, index) => {

                    let paxType = 'ADULT';
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
                            <PaxFormRender paxCount={index + 1} paxtype={paxType} />
                        </div>
                    );
                })}
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        </>
    )


    const PaxFormRender = ({ paxCount, paxtype }) => {
        const handleChange = (event, index) => {
            const newvalue = event.target.value;
            //setPaxList(prevState => {
            //    const updatedPaxList = [...prevState];
            //    updatedPaxList[index] = { ...updatedPaxList[index], PaxType: paxtype };
            //    return updatedPaxList;
            //});
            if (event.target.lang === 'title') {
                setPaxList(prevState => {
                    const updatedPaxList = [...prevState];
                    updatedPaxList[index] = { ...updatedPaxList[index], Title: newvalue };
                    return updatedPaxList;
                });
            }
            if (event.target.lang === 'fname') {
                setPaxList(prevState => {
                    const updatedPaxList = [...prevState];
                    updatedPaxList[index] = { ...updatedPaxList[index], FirstName: newvalue };
                    return updatedPaxList;
                });
            }
            if (event.target.lang === 'lname') {
                setPaxList(prevState => {
                    const updatedPaxList = [...prevState];
                    updatedPaxList[index] = { ...updatedPaxList[index], LastName: newvalue };
                    return updatedPaxList;
                });
            }
            if (event.target.lang === 'gender') {
                setPaxList(prevState => {
                    const updatedPaxList = [...prevState];
                    updatedPaxList[index] = { ...updatedPaxList[index], Gender: newvalue };
                    return updatedPaxList;
                });
            }
        };
        return (
            <div className='pax-render-cls'>
                <div className='container'>
                    <div className="row">
                        <div className='col'>
                            <h6>Passenger Type : {paxtype}</h6>
                        </div>
                        <div className="dropdown col">
                            <p>Title :</p>
                            <select className="form-dropdown" id="inpt-title" lang='title' onChange={(event) => handleChange(event, paxCount - 1)}>
                                <option value="">Select Title</option>
                                <option value="Mr">MR</option>
                                <option value="Mrs">Mrs</option>
                                <option value="Miss">Miss</option>
                            </select>
                        </div>
                        <div className="mb-3 col">
                            <label htmlFor="InputFirstName" className="form-label">First Name*</label>
                            <input type="text" className="form-control" id="InputFirstName" lang='fname' required onChange={(event) => handleChange(event, paxCount - 1)} />
                        </div>
                        <div className="mb-3 col">
                            <label htmlFor="InputLastName" className="form-label">Last Name*</label>
                            <input type="text" className="form-control" id="InputLastName" lang='lname' required onChange={(event) => handleChange(event, paxCount - 1)} />
                        </div>
                    </div>
                    <div className="row">
                        <div className="mb-3 col">
                            <label htmlFor="exampleInputEmail1" className="form-label">Email address*</label>
                            <input type="email" className="form-control" id="exampleInputEmail1" lang='email' required onChange={(event) => handleChange(event, paxCount - 1)} />
                        </div>
                        <div className="dropdown col">
                            <p>Gender : </p>
                            <select className="form-dropdown" id="inpt-gender" lang='gender' onChange={(event) => handleChange(event, paxCount - 1)}>
                                <option value="">Select Gender</option>
                                <option value="M">Male</option>
                                <option value="F">Female</option>
                                <option value="O">Other</option>
                            </select>
                        </div>
                    </div>
                </div>
            </div >

        )
    }
}
//export const PaxFormRender = ({ paxCount, paxtype }) => {
//    return (
//        <div className='pax-render-cls'>
//            <div className='container'>
//                <div className="row">
//                    <div className='col'>
//                        <h6>Passenger Type : {paxtype}</h6>
//                    </div>
//                    <div className="dropdown col">
//                        <p>Title :</p>
//                        <select className="form-dropdown" id="inpt-title" lang='title' onChange={(event) => handleChange(event, paxCount - 1)}>
//                            <option value="">Select Title</option>
//                            <option value="Mr">MR</option>
//                            <option value="Mrs">Mrs</option>
//                            <option value="Miss">Miss</option>
//                        </select>
//                    </div>
//                    <div className="mb-3 col">
//                        <label htmlFor="InputFirstName" className="form-label">First Name*</label>
//                        <input type="text" className="form-control" id="InputFirstName" lang='fname' required onChange={(event) => handleChange(event, paxCount - 1)} />
//                    </div>
//                    <div className="mb-3 col">
//                        <label htmlFor="InputLastName" className="form-label">Last Name*</label>
//                        <input type="text" className="form-control" id="InputLastName" lang='lname' required onChange={(event) => handleChange(event, paxCount - 1)} />
//                    </div>
//                </div>
//                <div className="row">
//                    <div className="mb-3 col">
//                        <label htmlFor="exampleInputEmail1" className="form-label">Email address*</label>
//                        <input type="email" className="form-control" id="exampleInputEmail1" lang='email' required onChange={(event) => handleChange(event, paxCount - 1)} />
//                    </div>
//                    <div className="dropdown col">
//                        <p>Gender : </p>
//                        <select className="form-dropdown" id="inpt-gender" lang='gender' onChange={(event) => handleChange(event, paxCount - 1)}>
//                            <option value="">Select Gender</option>
//                            <option value="M">Male</option>
//                            <option value="F">Female</option>
//                            <option value="O">Other</option>
//                        </select>
//                    </div>
//                </div>
//            </div>
//        </div >

//    )
//}
export default FlightPassenger;