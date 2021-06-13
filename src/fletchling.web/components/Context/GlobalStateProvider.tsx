import { ReactChild, ReactChildren, useEffect, useMemo, useState } from 'react';

import GlobalStateContext, {
  GlobalState,
  initialGlobalState
} from '@/components/Context/GlobalStateContext';
import { firebaseApp } from '@/lib/firebase';

interface Props {
  children: ReactChild | ReactChildren;
}

const GlobalStateProvider = ({ children }: Props) => {
  const [globalState, setGlobalState] = useState<GlobalState>(initialGlobalState);
  const contextValue = useMemo(
    () => ({ globalState, setGlobalState }),
    [globalState, setGlobalState]
  );

  useEffect(() => {
    const unsubscribe = firebaseApp.auth().onAuthStateChanged((user) => {
      setGlobalState({ ...globalState, isLoading: true });

      if (user) {
        user.getIdToken(true).then((token) => {
          setGlobalState({
            ...globalState,
            isLoading: false,
            currentUser: { uid: user.uid, token: token }
          });
        });
      } else {
        setGlobalState({ ...globalState, isLoading: false, currentUser: null });
      }
    });

    return unsubscribe;
  }, []);

  return <GlobalStateContext.Provider value={contextValue}>{children}</GlobalStateContext.Provider>;
};

export default GlobalStateProvider;
