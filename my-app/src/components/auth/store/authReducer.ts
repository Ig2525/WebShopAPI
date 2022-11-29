import { AuthActionTypes, AuthState } from "./types";
const initialState : AuthState={
    isAuth: false
}
export const authReducer = (state=initialState, action : any) : AuthState => {
    switch(action.type){
        case AuthActionTypes.LOGIN_SUCCESS: {
            return {
                ...state,
                isAuth: true,
                user: {
                    ...action.payload
                }
            };
        }
    }
    return state;
}