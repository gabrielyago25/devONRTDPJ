import axios from "axios";

export const authApi = axios.create({
  baseURL: "http://localhost:5001/api",
});

export const regApi = axios.create({
  baseURL: "http://localhost:5002/api",
});