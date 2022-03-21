module.exports = (url) => {
  if (url.startsWith('/')) {
    url = url.substring(1)
  }
  return `${process.env.VUE_APP_API_SCHEMA}://${process.env.VUE_APP_API_URL}/${url}`
}
