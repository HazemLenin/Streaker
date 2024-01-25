import { parseCookies, setCookie, destroyCookie } from "nookies";
import { SET_TOKENS, SetTokensAction } from "../models/auth/SetTokensAction";
import {
	REMOVE_TOKENS,
	RemoveTokensAction,
} from "../models/auth/RemoveTokensAction";

const initialState = parseCookies().tokens
	? JSON.parse(parseCookies().tokens ?? "")
	: null;

function tokensReducer(
	state = initialState,
	action: SetTokensAction | RemoveTokensAction
) {
	switch (action.type) {
		case SET_TOKENS:
			// localStorage.setItem("tokens", JSON.stringify(action.payload));
			setCookie(null, "tokens", JSON.stringify(action.payload), { path: "/" });
			return action.payload;

		case REMOVE_TOKENS:
			// localStorage.removeItem("tokens");
			destroyCookie(null, "tokens", { path: "/" });
			return null;

		default:
			return state;
	}
}

export default tokensReducer;
