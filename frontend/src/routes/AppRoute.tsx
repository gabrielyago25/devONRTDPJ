import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { LoginPagina } from "../pages/Login/Login";
import { RegistrosPagina } from "../pages/Registros/Registros";
import { PrivateRoute } from "./PrivateRoute";
import { Main } from "../layouts/Main";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPagina />} />

        <Route
          element={
            <PrivateRoute>
              <Main />
            </PrivateRoute>
          }
        >
          <Route path="/registros" element={<RegistrosPagina />}/>
        </Route>
        <Route path="*" element={<Navigate to="/registros" replace/>} />
      </Routes>
    </BrowserRouter>
  );
}