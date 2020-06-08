function isDesignTime() {
  return (
    process.env.NODE_ENV !== "production" &&
    process.env.VUE_APP_INJECTED !== "true"
  );
}

export { isDesignTime };
