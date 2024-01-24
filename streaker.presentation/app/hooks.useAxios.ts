import axios from "axios";
import { useSelector, useDispatch } from "react-redux";
import { remove_tokens, set_tokens } from "./actions";
import allReducers from "./Reducers";
import tokensReducer from "./Reducers/tokensReducer";
import { IRootState } from "./store";

const baseURL = "https://localhost:7075";
function useAxios({ includeTokens = true } = {}) {
	const dispatch = useDispatch();
	const tokens = useSelector((state: IRootState) => state.tokens);

	const axiosInstance = axios.create({
		baseURL,
		headers: {
			"Content-Type": "application/json",
		},
		// update
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

			localStorage.setItem("tokens", JSON.stringify(response?.data));

			dispatch(set_tokens(response?.data));

			res.config.headers.Authorization = `Bearer ${response?.data.token}`;

			return axiosInstance(res.config);
		});
	}

	return axiosInstance;
}

export default useAxios;
