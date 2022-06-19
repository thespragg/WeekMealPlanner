/// <reference types="vite/client" />
import { ComponentCustomProperties } from 'vue';

declare module '*.vue' {
  import { DefineComponent } from 'vue';
  // eslint-disable-next-line @typescript-eslint/no-explicit-any, @typescript-eslint/ban-types
  const component: DefineComponent<{}, {}, any>;
  export default component;
}

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    //
  }
}

interface ImportMetaEnv {
  readonly VITE_API_URL: string;
  readonly NODE_ENV: string;
  readonly VITE_SOCKET_URL: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}