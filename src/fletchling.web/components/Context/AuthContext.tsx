import { createContext } from 'react';

interface AuthContext {
  uid: string;
}

const AuthContext = createContext<AuthContext>({} as AuthContext);

export default AuthContext;
