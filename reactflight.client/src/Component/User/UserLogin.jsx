import { useState } from "react";
function UserLogin() {
    const [userCredential, setUserCredential] = useState({
        username: '',
        password: ''
    });
    const handleInput = (prop, value) => {
        const userdata = [userCredential];
        setUserCredential(prevstate => ({
            ...prevstate, [prop]: value
        })
        );
    }
    const handleSubmit = (event) => {
        event.preventDefault();
        if (!userCredential.username) {
            alert('UserName is Empty!');
            return;
        }
        if (!userCredential.password) {
            alert('Password is Empty!')
            return;
        }
        console.log(userCredential);
    }
    return (
        <>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="exampleInputEmail1">Email address</label>
                    <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email" onInput={(event) => handleInput('username', event.target.value)} />
                    <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                </div>
                <div className="form-group">
                    <label htmlFor="exampleInputPassword1">Password</label>
                    <input type="password" className="form-control" id="exampleInputPassword1" placeholder="Password" onInput={(event) => handleInput('password', event.target.value)} />
                </div>
                <div className="form-group form-check">
                    <input type="checkbox" className="form-check-input" id="exampleCheck1" />
                    <label className="form-check-label" htmlFor="exampleCheck1">Check me out</label>
                </div>
                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        </>
    );
}
export default UserLogin;