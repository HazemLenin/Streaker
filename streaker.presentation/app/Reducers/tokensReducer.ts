const initialState = localStorage.getItem("tokens")
	? JSON.parse(localStorage.getItem("tokens") ?? "")
	: null;

function tokensReducer(
	state = initialState,
	action: SetTokensAction | RemoveTokensAction
) {
	switch (action.type) {
		case SET_TOKENS:
			localStorage.setItem("tokens", JSON.stringify(action.payload));
			return action.payload;

		case REMOVE_TOKENS:
			localStorage.removeItem("tokens");
			return null;

		default:
			return state;
	}
}

export default tokensReducer;
