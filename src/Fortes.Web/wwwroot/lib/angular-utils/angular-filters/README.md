#angular-filters
## Collection of Angularjs filters

### Available Filters :
1. **[File Size Filter](#filesizefilter):** A filter which takes file size in bytes and returns a formatted string.
2. **[Search and Order Array](#searchAndOrderArray): Upcoming** A filter which combines filters: **orderBy**+**filter** and adds regex matching for better search and/or ordering.
3. **[Object To Get Query](#objectToGetQuery): Later** Covert object to get query string.
4. **[Get Query String To Object](#getQueryToObject): Later** Extract query params from a url and store in object.
5. **[Join Array of String](#joinArray): Later** join array of strings with given separator. same as array,join but implemented as a filter
6. **[Manipulate](#manipulate): Later** takes a function defined on scope and any number of arguments, applies the passes function to data and returns data
7. **[Global Objects in Html bindings](#globalObjects): Later** Making javascript globals available in html bindings, takes the name of global as the parameter
8. **[Number Filter with Notation and separator control](): Later**  
9. **More Filters** to Come.

####File Size Filter<a name="filesizefilter"></a>
#####Contents
- [Usage](#filesizefilterUsage)
- [Usage Options](#filesizefilterUsageOptions)
- [Examples](#filesizefilterExamples)
- [Other Information](#filesizefilterOtherInformation)

#####Usage<a name="filesizefilterUsage"></a>
- Include filesizefilter.js script in your Angularjs project
- Inject 'filesize-filter' module into your module.
- Use filesize as a filter in your view
- example: your data binding can look like **{{sizeOfFile | filesize}}**

#####Usage Options<a name="filesizefilterUsageOptions"></a>
- You can specify custom prefix (KB or MB or anything valid). Returned Size is calculated as per prefix. 
[See List of Valid Prefixes](#supportedPrefixes)

- You can specify custom precision after decimal point as well.
Your 

**NOTE:** I know the term **prefix** doesn't suit, but anyways.

#####Examples<a name="filesizefilterExamples"></a>


	<tr>
		<td>{{size}}</td>
		<td>{{size|filesize}}</td>
		<td>{{size|filesize:'prefix as string':precisionAsNumber}}</td>
		<td>{{size|filesize:' k':3}}</td>
		<td>{{size|filesize:' kiloByte':2}}</td>
		<td>{{size|filesize:' G':5}}</td>
		<td>{{size|filesize:' gigaByte':2}}</td>
	</tr>
    
**For a clearer example check the [example/filesize-filter/filesizefilter.html](./example/filesize-filter/filesizefilter.html)**

![Example Image](./example/filesize-filter/ScreenShot.png)

<a name="supportedPrefixes"></a>**Supported Prefixes**

- k, or any string beginning with k or K : Size is formatted as KiloByte i.e. divided by 1024 and then number is postfixed with your given prefix and returned as string
- Just like k, other prefixes are supported 
- Prefix beginnning with **M** or **m** - MegaByte
- **G** or **g** GigaByte
- **T** or **t** TeraByte
- **P** or **p** PetaByte

#####Other Information<a name="filesizefilterOtherInformation"></a>
- [Source](./src/filesizefilter.js)
- **Dependency:** [Angularjs](https://angularjs.org)

####Search And Order Array<a name="searchAndOrderArray"></a>

####Object To Get Query Filter<a name="objectToGetQuery"></a>