import "material-design-icons/iconfont/material-icons.css";
import "@mdi/font/css/materialdesignicons.css";
import "font-awesome/css/font-awesome.css";

import Vue_Router from "vue-router";
import { router } from "@/neutronium/route";
import VueI18n from "vue-i18n";
import messages from "./message";
import Notifications from "vue-notification";
import { formatTime, timeSpan, totalSeconds } from "./filter/time";
import { track } from "./filter/track";
import iconButton from "./components/button/iconButton";
import textButton from "./components/button/textButton";

import Vuetify, {
  VApp,
  VAppBar,
  VAlert,
  VCard,
  VCardText,
  VCardTitle,
  VCardActions,
  VContent,
  VContainer,
  VDivider,
  VNavigationDrawer,
  VFooter,
  VFlex,
  VLayout,
  VList,
  VListItem,
  VListItemTitle,
  VListItemAction,
  VListItemContent,
  VBtn,
  VIcon,
  VImg,
  VToolbar,
  VDialog,
  VTextField,
  VAppBarNavIcon,
  VToolbarTitle,
  VSpacer,
  VSlider
} from "vuetify/lib";

function install(Vue) {
  Vue.use(Vuetify, {
    components: {
      VApp,
      VAppBar,
      VAlert,
      VCard,
      VCardText,
      VCardTitle,
      VCardActions,
      VContent,
      VContainer,
      VDivider,
      VNavigationDrawer,
      VFooter,
      VFlex,
      VLayout,
      VList,
      VListItem,
      VListItemTitle,
      VListItemAction,
      VListItemContent,
      VBtn,
      VIcon,
      VImg,
      VToolbar,
      VDialog,
      VTextField,
      VAppBarNavIcon,
      VToolbarTitle,
      VSpacer,
      VSlider
    }
  });

  Vue.use(Vue_Router);
  Vue.use(VueI18n);
  Vue.use(Notifications);
  Vue.component("iconButton", iconButton);
  Vue.component("textButton", textButton);
  Vue.filter("timeSpan", timeSpan);
  Vue.filter("formatTime", formatTime);
  Vue.filter("track", track);
  Vue.filter("totalSeconds", totalSeconds);
}

/*eslint no-unused-vars: ["error", { "args": "none" }]*/
function vueInstanceOption(vm) {
  const i18n = new VueI18n({
    locale: "en-US", // set locale
    messages // set locale messages
  });

  const vuetify = new Vuetify({
    theme: {
      dark: true
    }
  });

  //Return vue global option here, such as vue-router, vue-i18n, mix-ins, ....
  return {
    router,
    vuetify,
    i18n
  };
}

export { install, vueInstanceOption };
