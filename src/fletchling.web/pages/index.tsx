import { useMemo, useState } from 'react';

import TimelineContext from '@/components/Context/TimelineContext';
import Layout from '@/components/Layout';
import Search from '@/components/SearchUser/Search';
import TimelineList from '@/components/Timeline/TimelineList';
import { app, twitterProvider } from '@/lib/firebase';

const IndexPage = () => {
  const [savedTimelines, setSavedTimelines] = useState<string[]>([] as string[]);
  const contextValue = useMemo(
    () => ({ savedTimelines, setSavedTimelines }),
    [savedTimelines, setSavedTimelines]
  );

  const onClick = () => {
    console.log('test');
    app
      .auth()
      .signInWithPopup(twitterProvider)
      .then((result) => console.log(result))
      .catch((error) => console.log(error));
  };

  return (
    <Layout title='Home'>
      <TimelineContext.Provider value={contextValue}>
        <Search />
        <TimelineList />
        <button onClick={onClick}>test</button>
      </TimelineContext.Provider>
    </Layout>
  );
};

export default IndexPage;
