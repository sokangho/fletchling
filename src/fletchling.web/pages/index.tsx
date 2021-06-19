import { useContext, useEffect, useMemo, useState } from 'react';

import GlobalStateContext from '@/components/Context/GlobalStateContext';
import TimelineContext from '@/components/Context/TimelineContext';
import Layout from '@/components/Layout';
import Search from '@/components/SearchUser/Search';
import TimelineList from '@/components/Timeline/TimelineList';
import TimelineGroup from '@/interfaces/TimelineGroup';
import TimelineRequest from '@/interfaces/TimelineRequest';
import { patcher } from '@/lib/axios';
import { fetcher } from '@/lib/axios';

const IndexPage = () => {
  const [savedTimelines, setSavedTimelines] = useState<string[]>([] as string[]);

  const {
    globalState: { currentUser }
  } = useContext(GlobalStateContext);
  const token = currentUser?.token;

  useEffect(() => {
    const fetchData = async () => {
      // If user is authenticated, fetch saved timelines from db
      if (currentUser != null) {
        const res = await fetcher<TimelineGroup>('/timeline?timelinegroupname=All', token);
        setSavedTimelines(res.timelines);
      }
      // If user is not authenticated
      else {
        setSavedTimelines([]);
      }
    };

    fetchData();
  }, [currentUser]);

  const updateSavedTimelines = async (savedTimelines: string[]) => {
    if (currentUser) {
      const data: TimelineRequest = {
        uid: currentUser.uid,
        timelines: savedTimelines,
        groupName: 'All'
      };
      setSavedTimelines(savedTimelines);
      await patcher<TimelineRequest>('/timeline', data, token);
    }
  };

  const contextValue = useMemo(() => ({ savedTimelines, updateSavedTimelines }), [savedTimelines]);

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
