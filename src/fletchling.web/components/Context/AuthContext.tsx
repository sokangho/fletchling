import { createContext } from 'react';

export interface AuthUser {
  currentUser: {
    uid: string;
    token: string;
  } | null;
}

const AuthContext = createContext<AuthUser>({ currentUser: null });

export default AuthContext;
