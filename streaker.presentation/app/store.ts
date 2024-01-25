import { configureStore } from "@reduxjs/toolkit";
import allReducers from "./reducers";

interface Window {
	__REDUX_DEVTOOLS_EXTENSION__?: () => (args: any) => void;
}

const store = configureStore({
	reducer: allReducers,
});

export type IRootState = ReturnType<typeof allReducers>;

export default store;
