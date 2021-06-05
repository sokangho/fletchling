import { useMemo, useState } from 'react';

import Layout from '@/components/Layout';
import Search from '@/components/SearchUser/Search';
import TimelineList from '@/components/Timeline/TimelineList';
import TimelineContext from '@/components/TimelineContext';

const IndexPage = () => {
  const [savedTimelines, setSavedTimelines] = useState<string[]>([] as string[]);
  const contextValue = useMemo(
    () => ({ savedTimelines, setSavedTimelines }),
    [savedTimelines, setSavedTimelines]
  );

  return (
    <Layout title='Home'>
      <TimelineContext.Provider value={contextValue}>
        <Search />
        <TimelineList />
      </TimelineContext.Provider>
    </Layout>
  );
};

export default IndexPage;
