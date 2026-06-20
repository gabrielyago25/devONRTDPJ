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

regApi.interceptors.response.use((response) => response,(error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem("token");
      if (window.location.pathname !== "/login") {
        window.location.replace("/login");
      }
    }

    return Promise.reject(error);
  }
);