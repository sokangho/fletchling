import { createContext, Dispatch, SetStateAction } from 'react';

export interface GlobalState {
  isLoading: boolean;
  currentUser: {
    uid: string;
    token: string;
  } | null;
}

export interface GlobalStateContext {
  globalState: GlobalState;
  setGlobalState: Dispatch<SetStateAction<GlobalState>>;
}

const initialGlobalState = { isLoading: false, currentUser: null };

const GlobalStateContext = createContext<GlobalStateContext>({
  globalState: initialGlobalState
} as GlobalStateContext);

export default GlobalStateContext;
export { initialGlobalState };
