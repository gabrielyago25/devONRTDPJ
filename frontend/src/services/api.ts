import axios from "axios";

export const authApi = axios.create({
  baseURL: "http://localhost:5001/api",
});

export const regApi = axios.create({
  baseURL: "http://localhost:5002/api",
});

regApi.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});