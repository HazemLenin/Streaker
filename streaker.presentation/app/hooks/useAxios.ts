import axios from "axios";
import { useSelector, useDispatch } from "react-redux";
import { remove_tokens, set_tokens } from "../actions";
import { IRootState } from "../store";
import { setCookie, parseCookies } from "nookies";

const baseURL = "https://localhost:7075";
function useAxios({ includeTokens = true } = {}) {
	const dispatch = useDispatch();
	const tokens = useSelector((state: IRootState) => state.tokens);

	const axiosInstance = axios.create({
		baseURL,
		headers: {
			"Content-Type": "application/json",
			Authorization: `Bearer ${tokens?.token}`,
		},
	});

	if (includeTokens) {
		axiosInstance.interceptors.response.use(async (res) => {
			// if unauthorized post refresh token and try again the request with the new token
			if (res.status != 401) return res;

			const response = await axios
				.post(`/api/auth/refresh`, {
					refreshToken: tokens.refreshToken,
				})
				.catch((err) => {
					if (err.response.status === 401) {
						// logout the user
						dispatch(remove_tokens());
					}
				});

			// localStorage.setItem("tokens", JSON.stringify(response?.data));
			setCookie(null, "tokens", JSON.stringify(response?.data), { path: "/" });

			dispatch(set_tokens(response?.data.data));

			res.config.headers.Authorization = `Bearer ${response?.data.token}`;

			return axiosInstance(res.config);
		});
	}

	return axiosInstance;
}

export default useAxios;
