export const SESSION_EXPIRED_KEY = "sessionExpired";

export function acessoExpirado() {
  sessionStorage.setItem(SESSION_EXPIRED_KEY, "true");
  localStorage.removeItem("token");
}

