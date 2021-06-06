import { ReactChild, ReactChildren, useEffect, useState } from 'react';

import AuthContext, { AuthUser } from '@/components/Context/AuthContext';
import { firebaseApp } from '@/lib/firebase';

interface Props {
  children: ReactChild | ReactChildren;
}

const AuthProvider = ({ children }: Props) => {
  const [currentUser, setCurrentUser] = useState<AuthUser>({ currentUser: null });

  useEffect(() => {
    const unsubscribe = firebaseApp.auth().onAuthStateChanged((user) => {
      user
        ? setCurrentUser({ currentUser: { uid: user.uid } })
        : setCurrentUser({ currentUser: null });
    });

    return unsubscribe;
  }, []);

  return <AuthContext.Provider value={currentUser}>{children}</AuthContext.Provider>;
};

export default AuthProvider;
