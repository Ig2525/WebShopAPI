import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import http from "../../../http_common";
import { IUserItem } from "./types";

    const MyAccountPage = () => {

    const [list, setList] = useState<Array<IUserItem>>([
    ]);

    useEffect(() => {        
        
        http.get<Array<IUserItem>>("/api/users")
            .then(resp => {
                console.log("server response", resp);
                setList(resp.data);
            });
    },[]);

    const data = list.map(user => 
        <tr key={user.id}>
            <td>{user.id}</td>
            <td>{user.fullName}</td>
            <td>{user.phone}</td>
            <td>{user.email}</td>
            <td>
                <img src={http.getUri()+user.image} alt="фото" width="75" />
            </td>
        </tr>
    );

    return (
        <>
            <h1>Мій акаунт</h1>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Ім'я</th>
                        <th scope="col">Телефон</th>
                        <th scope="col">Email</th>
                        <th scope="col">Фото</th>
                    </tr>
                </thead>
                <tbody>{data}</tbody>
            </table>
        </>
    );
}

export default MyAccountPage;

/* const [state, setState] = useState<IAuthMyAccaunt>({
    email: "",
    firstname: "",
    lastname: "",
    phone: "",
    image: "",
    password: ""
}); */