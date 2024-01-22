import tokensReducer from "./tokensReducer";

import { combineReducers } from "redux";

const allReducers = combineReducers({
	tokens: tokensReducer,
});

export default allReducers;
