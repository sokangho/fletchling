import { createContext, ReactNode, useMemo, useState } from 'react';

interface SavedTimelines {
  savedTimelines: string[];
  updateSavedTimelines: (savedTimelines: string[]) => Promise<void>;
}

interface Props {
  children: ReactNode;
}

const TimelineContext = createContext<SavedTimelines>({} as SavedTimelines);

function TimelineContextProvider({ children }: Props) {
  const [savedTimelines, setSavedTimelines] = useState<string[]>([]);

  const updateSavedTimelines = async (savedTimelines: string[]) => {
    setSavedTimelines(savedTimelines);
  };

  const contextValue = useMemo(
    () => ({
      savedTimelines,
      updateSavedTimelines
    }),
    [savedTimelines]
  );

  return <TimelineContext.Provider value={contextValue}>{children}</TimelineContext.Provider>;
}

export { TimelineContext, TimelineContextProvider };
