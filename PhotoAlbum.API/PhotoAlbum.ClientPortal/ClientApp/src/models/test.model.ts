import { EffectsCommandMap } from 'dva';
import { AnyAction, Reducer } from 'redux';

export interface ActivityState {
  openAlerts: string[];
}

const defaultActivityState: ActivityState = {
  openAlerts: [],
};

type Effect = (
  action: AnyAction,
  effects: EffectsCommandMap & { select: <T>(func: (state: ActivityState) => T) => T },
) => void;

export interface ActivityModel {
  namespace: string;
  state: ActivityState;
  effects: {
    fetchAlerts: Effect;
  };
  reducers: {
    clear: Reducer<ActivityState>;
  };
}

const Model: ActivityModel = {
  namespace: 'activity',

  state: defaultActivityState,

  effects: {
    *fetchAlerts({ payload, onCompletedCallback }, { call, put }) {
      // do api call
    },
  },

  reducers: {
    clear() {
      return defaultActivityState;
    },
  },
};

export default Model;
