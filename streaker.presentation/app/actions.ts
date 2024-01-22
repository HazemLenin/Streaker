export function set_tokens(data: Tokens): SetTokensAction {
	return {
		type: "SET_TOKENS",
		payload: data,
	};
}

export function remove_tokens(): RemoveTokensAction {
	return {
		type: "REMOVE_TOKENS",
	};
}
