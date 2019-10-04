# import libraries
import urllib2
import json
from bs4 import BeautifulSoup
from unidecode import unidecode
from datetime import datetime

# Get the versions
def get_version_slug_map(html):

    # Take out just the part of each anchor tag that we want
    version_links = html.find_all('a', attrs={'class': 'db pb2 lh-copy yv-green link'})
    versions = {}

    for thumbnail in version_links:
        chunks = thumbnail.get('href').replace('/versions/', '').split('-')
        version_number = unidecode(chunks[0])
        if len(chunks) > 1:
            version_slug = unidecode(chunks[1])

        # grab just the part that we need
        versions[version_number] = unidecode(version_slug) or thumbnail.get("title").replace("Read Version: ", '')

    return versions

# Get the versions
def get_version_display_name_map(html):

    # Take out just the part of each anchor tag that we want
    version_links = html.find_all('a', attrs={'class': 'db pb2 lh-copy yv-green link'})
    versions = {}

    for thumbnail in version_links:
        chunks = thumbnail.get('href').replace('/versions/', '').split('-')
        version_number = unidecode(chunks[0])

        # grab just the part that we need
        versions[thumbnail.get("title").replace("Read Version: ", '')] = version_number

    return versions

# Get the languages
def get_languages(html):

    # Take out just the part of each anchor tag that we want
    version_links = html.find_all('a', attrs={'class': 'db lh-copy yv-green link'})
    versions = {}

    for thumbnail in version_links:
        chunks = thumbnail.get('href').replace('/languages/', '')
        code = unidecode(chunks)

        language_name = thumbnail.get("title").replace("Download the Bible in ", '').replace(' (%s)' %(code), '')

        # You can eaily remove the beginning part of this or case, to get only the display names for each one
        versions[code] = language_name

    return versions

def write_data_to_file(data, req_filename):
    print "Generating txt File..."

    # write things down in a file, because it might be too big to fit on the console window
    filename = "{0}.txt".format(req_filename)
    filehandle = open(filename, "w")
    filehandle.write(json.dumps(data))  
    filehandle.close()

    print "{0} successfully created.".format(filename)
    return

## scrape a URL
def scrape_page(url):

    hdr = {'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11',
       'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8',
       'Accept-Charset': 'ISO-8859-1,utf-8;q=0.7,*;q=0.3',
       'Accept-Encoding': 'none',
       'Accept-Language': 'en-US,en;q=0.8',
       'Connection': 'keep-alive'}

    req = urllib2.Request(url, headers=hdr)

    # query the website and return the html to the variable 'page'
    page = urllib2.urlopen(req)

    # parse the html using beautiful soup and store in variable 'soup'
    soup = BeautifulSoup(page, 'html.parser')

    return soup

def main():

    # scraping page
    print "Scraping page..."
    html = scrape_page('https://www.bible.com/versions')

    print "\nGathering Versions"
    versions_data = get_version_slug_map(html)
    write_data_to_file(versions_data, "versions")

    version_slugs = get_version_display_name_map(html)
    write_data_to_file(version_slugs, "versionsSlugMap")

    # scraping page
    print "Scraping 2nd page..."
    html = scrape_page('https://www.bible.com/languages')

    print "\nGathering Versions"
    languages_list = get_languages(html)
    write_data_to_file(languages_list, "languagesMap")

    # print "\nGetting the slugs"
    # slugList = get_slugs(html)


# call the main method
main()