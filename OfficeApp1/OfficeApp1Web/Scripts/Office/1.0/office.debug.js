/* Office JavaScript API library */
/* Version: 16.0.1521.3006 */
/*
	Copyright (c) Microsoft Corporation.  All rights reserved.
*/

/*
	Your use of this file is governed by the Microsoft Services Agreement http://go.microsoft.com/fwlink/?LinkId=266419.
*/

var OSF=OSF || {};
OSF.ConstantNames={
	OfficeJS : "office.js",
	OfficeDebugJS : "office.debug.js",
	DefaultLocale : "en-us",
	LocaleStringLoadingTimeout : 2000,
	OfficeStringJS : "office_strings.debug.js",
	O15InitHelper  : "O15AppToFileMappingTable.debug.js"
};
OSF.InitializationHelper=function OSF_InitializationHelper(hostInfo, webAppState, context, settings, hostFacade) {
	this._hostInfo=hostInfo;
	this._webAppState=webAppState;
	this._context=context;
	this._settings=settings;
	this._hostFacade=hostFacade;
};
OSF.InitializationHelper.prototype.getAppContext=function OSF_InitializationHelper$getAppContext(wnd, gotAppContext) {
};
OSF.InitializationHelper.prototype.setAgaveHostCommunication=function OSF_InitializationHelper$setAgaveHostCommunication() {
};
OSF.InitializationHelper.prototype.prepareRightBeforeWebExtensionInitialize=function OSF_InitializationHelper$prepareRightBeforeWebExtensionInitialize(appContext) {
};
OSF.InitializationHelper.prototype.loadAppSpecificScriptAndCreateOM=function OSF_InitializationHelper$loadAppSpecificScriptAndCreateOM(appContext, appReady, basePath) {
};
OSF._OfficeAppFactory=(function OSF__OfficeAppFactory() {
	var _setNamespace=function OSF_OUtil$_setNamespace(name, parent) {
		if (parent && name && !parent[name]) {
			parent[name]={};
		}
	};
	_setNamespace("Office", window);
	_setNamespace("Microsoft", window);
	_setNamespace("Office", Microsoft);
	_setNamespace("WebExtension", Microsoft.Office);
	window.Office=Microsoft.Office.WebExtension;
			var _fallbackVersions={
				1   : "16",
				2   : "16",
				4   : "16",
				8   : "16",
				16  : "16",
				64  : "16",
				128 : "16",
				256 : "16"
			};
			var _appToScriptTable={
				"1-15" : "excel-15.debug.js",
				"2-15" : "word-15.debug.js",
				"4-15": "powerpoint-15.debug.js",
				"8-15" : "outlook-15.debug.js",
				"16-15" : "excelwebapp-15.debug.js",
				"64-15" : "outlookwebapp-15.debug.js",
				"128-15": "project-15.debug.js",
				"1-16" : "excel-16.debug.js",
				"2-16" : "word-16.debug.js",
				"4-16": "powerpoint-16.debug.js",
				"8-16" : "outlook-16.debug.js",
				"16-16" : "excelwebapp-16.debug.js",
				"64-16" : "outlookwebapp-16.debug.js",
				"128-16": "project-16.debug.js",
				"256-16": "accesswebapp-16.debug.js"
			};
	var _context={};
	var _settings={};
	var _hostFacade={};
	var _WebAppState={ id : null, webAppUrl : null, conversationID : null, clientEndPoint : null, wnd : window.parent, focused : false };
	var _hostInfo={ isO15: true, isRichClient: true, appName: -1, appVersion: "" };
	var _initializationHelper={};
	var _loadScript=function OSF_OUtil$_loadScript(url, callback, timeoutInMs) {
			var loadedScripts={};
			var defaultScriptLoadingTimeout=30000;
			if (url && callback) {
				var doc=window.document;
				var loadedScriptEntry=loadedScripts[url];
				if (!loadedScriptEntry) {
					var script=doc.createElement("script");
					script.type="text/javascript";
					loadedScriptEntry={ loaded: false, pendingCallbacks: [callback], timer: null };
					loadedScripts[url]=loadedScriptEntry;
					var onLoadCallback=function OSF_OUtil_loadScript$onLoadCallback() {
						if(loadedScriptEntry.timer !=null) {
							clearTimeout(loadedScriptEntry.timer);
							delete loadedScriptEntry.timer;
						}
						loadedScriptEntry.loaded=true;
						var pendingCallbackCount=loadedScriptEntry.pendingCallbacks.length;
						for (var i=0; i < pendingCallbackCount; i++) {
							var currentCallback=loadedScriptEntry.pendingCallbacks.shift();
							currentCallback();
						}
					};
					var onLoadError=function OSF_OUtil_loadScript$onLoadError() {
						delete loadedScripts[url];
						if(loadedScriptEntry.timer !=null) {
							clearTimeout(loadedScriptEntry.timer);
							delete loadedScriptEntry.timer;
						}
						var pendingCallbackCount=loadedScriptEntry.pendingCallbacks.length;
						for (var i=0; i < pendingCallbackCount; i++) {
							var currentCallback=loadedScriptEntry.pendingCallbacks.shift();
							currentCallback();
						}
					};
					if (script.readyState) {
						script.onreadystatechange=function () {
							if (script.readyState=="loaded" || script.readyState=="complete") {
								script.onreadystatechange=null;
								onLoadCallback();
							}
						};
					} else {
						script.onload=onLoadCallback;
					}
					script.onerror=onLoadError;
					timeoutInMs=timeoutInMs || defaultScriptLoadingTimeout;
					loadedScriptEntry.timer=setTimeout(onLoadError, timeoutInMs);
					script.src=url;
					doc.getElementsByTagName("head")[0].appendChild(script);
				} else if (loadedScriptEntry.loaded) {
					callback();
				} else {
					loadedScriptEntry.pendingCallbacks.push(callback);
				}
			}
	};
		var _parseXdmInfo=function OSF_OUtil$_parseXdmInfo() {
		   var xdmInfoKey='&_xdm_Info=';
		   var xdmSessionKeyPrefix='_xdm_';
		   var fragment=window.location.hash;
			var fragmentParts=fragment.split(xdmInfoKey);
			var xdmInfoValue=fragmentParts.length > 1 ? fragmentParts[fragmentParts.length - 1] : null;
			if (window.sessionStorage) {
				var sessionKeyStart=window.name.indexOf(xdmSessionKeyPrefix);
				if (sessionKeyStart > -1) {
					var sessionKeyEnd=window.name.indexOf(";", sessionKeyStart);
					if (sessionKeyEnd==-1) {
						sessionKeyEnd=window.name.length;
					}
					var sessionKey=window.name.substring(sessionKeyStart, sessionKeyEnd);
					if (xdmInfoValue) {
						window.sessionStorage.setItem(sessionKey, xdmInfoValue);
					} else {
						xdmInfoValue=window.sessionStorage.getItem(sessionKey);
					}
				}
			}
			return xdmInfoValue;
		};
	var _parseHostInfo=function OSF__OfficeAppFactory$_parseHostInfo() {
		var hostInfoValue;
		var hostInfo="_host_Info=";
		var searchString=window.location.search;
		if (searchString) {
			var hostInfoParts=searchString.split(hostInfo);
			if (hostInfoParts.length > 1) {
	            var hostInfoValueRestString=hostInfoParts[1];
	            var separatorRegex=new RegExp("/[&#]/g");
	            var hostInfoValueParts=hostInfoValueRestString.split(separatorRegex);
	            if (hostInfoValueParts.length > 0) {
		            hostInfoValue=hostInfoValueParts[0];
	            }
			}
		}
		return hostInfoValue;
	};
	var _retrieveHostInfo=function OSF__OfficeAppFactory$_retrieveHostInfo() {
		var hostInfoValue=_parseHostInfo();
		var xdmInfoValue=_parseXdmInfo();
		if (hostInfoValue) {
			_hostInfo.isO15=false;
			var items=hostInfoValue.split('|');
			_hostInfo.appName=items[0];
			_hostInfo.appVersion=items[1];
		} else {
			_hostInfo.isO15=true;
			if (xdmInfoValue) {
				_hostInfo.isRichClient=false;
			} else {
				_hostInfo.isRichClient=true;
			}
		}
		if (xdmInfoValue) {
			var xdmItems=xdmInfoValue.split('|');
			if (xdmItems !=undefined && xdmItems.length===3) {
				_WebAppState.conversationID=xdmItems[0];
				_WebAppState.id=xdmItems[1];
				_WebAppState.webAppUrl=xdmItems[2];
			}
		}
	};
	var getAppContextAsync=function OSF__OfficeAppFactory$getAppContextAsync(wnd, gotAppContext) {
		_initializationHelper.getAppContext(wnd, gotAppContext);
	};
	var initialize=function OSF__OfficeAppFactory$initialize() {
		_retrieveHostInfo();
		var getScriptBase=function OSF__OfficeAppFactory_initialize$getScriptBase(scriptSrc, scriptNameToCheck) {
	        var scriptBase, indexOfJS;
	        scriptSrc=scriptSrc.toLowerCase();
	        scriptNameToCheck=scriptNameToCheck.toLowerCase();
	        indexOfJS=scriptSrc.indexOf(scriptNameToCheck);
	        if (indexOfJS >=0 && indexOfJS===(scriptSrc.length - scriptNameToCheck.length) && (indexOfJS===0 || scriptSrc.charAt(indexOfJS-1)==='/' || scriptSrc.charAt(indexOfJS-1)==='\\')) {
		        scriptBase=scriptSrc.substring(0, indexOfJS);
	        }
	        return scriptBase;
		};
		var scripts=document.getElementsByTagName("script") || [];
		var scriptsCount=scripts.length;
		var officeScripts=[OSF.ConstantNames.OfficeJS, OSF.ConstantNames.OfficeDebugJS];
		var officeScriptsCount=officeScripts.length;
		var i, j, basePath;
		for (i=0; !basePath && i < scriptsCount; i++) {
	        if (scripts[i].src) {
		        for(j=0; !basePath && j < officeScriptsCount; j++) {
			        basePath=getScriptBase(scripts[i].src, officeScripts[j]);
		        }
	        }
		}
		if (!basePath) throw "Office Web Extension script library file name should be "+OSF.ConstantNames.OfficeJS+" or "+OSF.ConstantNames.OfficeDebugJS+".";
		var numberOfTimeForMsAjaxTries=500;
		var timerId;
		var loadLocaleStringsAndAppSpecificCode=function OSF__OfficeAppFactory_initialize$loadLocaleStringsAndAppSpecificCode() {
			if (typeof (Sys) !=='undefined' && typeof (Type) !=='undefined' &&
				Sys.StringBuilder && typeof (Sys.StringBuilder)==="function" &&
				Type.registerNamespace && typeof (Type.registerNamespace)==="function" &&
				Type.registerClass && typeof (Type.registerClass)==="function") {
				_initializationHelper=new OSF.InitializationHelper(_hostInfo, _WebAppState, _context, _settings, _hostFacade);
				_initializationHelper.setAgaveHostCommunication();
				getAppContextAsync(_WebAppState.wnd, function (appContext) {
					var postLoadLocaleStringInitialization=function OSF__OfficeAppFactory_initialize$postLoadLocaleStringInitialization() {
						var retryNumber=100;
						var t;
						function appReady() {
							if (Microsoft.Office.WebExtension.initialize !=undefined) {
	                            _initializationHelper.prepareRightBeforeWebExtensionInitialize(appContext);
	                            if (t !=undefined) window.clearTimeout(t);
							} else if (retryNumber==0) {
	                            clearTimeout(t);
	                            throw "Office.js has not been fully loaded yet. Please try again later or make sure to add your initialization code on the Office.initialize function.";
							} else {
	                            retryNumber--;
	                            t=window.setTimeout(appReady, 100);
							}
						};
						_initializationHelper.loadAppSpecificScriptAndCreateOM(appContext, appReady, basePath);
					};
					var fallbackLocaleTried=false;
					var loadLocaleStringCallback=function OSF__OfficeAppFactory_initialize$loadLocaleStringCallback() {
	                    if (typeof Strings=='undefined' || typeof Strings.OfficeOM=='undefined') {
		                    if(!fallbackLocaleTried) {
			                    fallbackLocaleTried=true;
			                    var fallbackLocaleStringFile=basePath+OSF.ConstantNames.DefaultLocale+"/"+OSF.ConstantNames.OfficeStringJS;
			                    _loadScript(fallbackLocaleStringFile, loadLocaleStringCallback);
		                    } else {
			                    throw "Neither the locale, "+appContext.get_appUILocale().toLowerCase()+", provided by the host app nor the fallback locale "+OSF.ConstantNames.DefaultLocale+" are supported.";
		                    }
	                    } else {
		                    fallbackLocaleTried=false;
		                    postLoadLocaleStringInitialization();
	                    }
					};
					var localeStringFile=OSF.OUtil.formatString("{0}{1}/{2}", basePath, appContext.get_appUILocale().toLowerCase(), OSF.ConstantNames.OfficeStringJS);
					_loadScript(localeStringFile, loadLocaleStringCallback, OSF.ConstantNames.LocaleStringLoadingTimeout);
				});
			} else if (numberOfTimeForMsAjaxTries===0) {
				clearTimeout(timerId);
				throw "MicrosoftAjax.js is not loaded successfully.";
			} else {
				numberOfTimeForMsAjaxTries--;
				timerId=window.setTimeout(loadLocaleStringsAndAppSpecificCode, 100);
			}
		}
		if (_hostInfo.isO15) {
   	        _loadScript(basePath+OSF.ConstantNames.O15InitHelper, loadLocaleStringsAndAppSpecificCode);
		} else {
			_loadScript(basePath+_appToScriptTable[_hostInfo.appName+"-"+_hostInfo.appVersion], loadLocaleStringsAndAppSpecificCode);
		}
		window.confirm=function OSF__OfficeAppFactory_initialize$confirm (message) {
			throw 'Function window.confirm is not supported.';
		};
		window.alert=function OSF__OfficeAppFactory_initialize$alert (message) {
			throw 'Function window.alert is not supported.';
		};
		window.prompt=function OSF__OfficeAppFactory_initialize$prompt (message, defaultvalue) {
			throw 'Function window.prompt is not supported.';
		};
	};
	initialize();
	return {
		getId : function OSF__OfficeAppFactory$getId() {return _WebAppState.id;},
		getClientEndPoint : function OSF__OfficeAppFactory$getClientEndPoint() { return _WebAppState.clientEndPoint; },
		getWebAppState : function OSF__OfficeAppFactory$getWebAppState() { return _WebAppState; },
		getContext: function OSF__OfficeAppFactory$getContext() { return _context; },
		setContext: function OSF__OfficeAppFactory$setContext(context) {_context=context;},
		getHostFacade: function OSF__OfficeAppFactory$getHostFacade() { return _hostFacade; },
		setHostFacade: function setHostFacade(hostFacade) {_hostFacade=hostFacade;}
	};
})();

