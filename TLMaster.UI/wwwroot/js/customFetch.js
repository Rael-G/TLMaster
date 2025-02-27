window.customFetch = async function (url, method, headers, body, credentialsMode) {
    const response = await fetch(url, {
        method: method,
        headers: headers,
        body: body,
        credentials: credentialsMode
    });

    let responseBody;
    try {
        responseBody = await response.text();
    } catch {
        responseBody = null;
    }

    return {
        status: response.status,
        statusText: response.statusText,
        headers: Object.fromEntries(response.headers.entries()),
        body: responseBody
    };
};
