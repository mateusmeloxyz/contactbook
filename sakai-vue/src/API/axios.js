import axios from "axios";

const axiosAPI = axios.create({
    baseURL: "http://localhost:8080/api/",
    timeout: 20000,
    headers: {
        //'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json',
        'Accept': 'application/json',
    },
});

export default axiosAPI;
