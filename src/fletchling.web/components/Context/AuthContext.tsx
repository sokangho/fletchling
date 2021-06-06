import { createContext } from 'react';

export interface AuthUser {
  currentUser: Record<string, unknown> | null;
}

const AuthContext = createContext<AuthUser>({ currentUser: null });

export default AuthContext;
