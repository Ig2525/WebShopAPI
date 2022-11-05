import axios from "axios";


const http= axios.create({
    baseURL:"http://localhost:5165",
    headers: {
        "Content-type": "aplication/json"
    }
});

export default http;