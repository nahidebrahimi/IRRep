using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheApplication
{
    public class QueryParser
    {
        PorterStemmerAlgorithm.PorterStemmer myStemmer; 
        System.Collections.Generic.Dictionary<string, int> tokenCount; 
        public string[] stopWords = {"a", "an", "and", "are", "as", "at", "be", "but", "by","for", "if", "in", "into", "is", "it","no", "not", "of", "on", "or", "such","that", "the", "their", "then", "there", "these","they", "this", "to", "was", "will", "with"}; // for challange activity

        public QueryParser()
        {
            myStemmer = new PorterStemmerAlgorithm.PorterStemmer();
            //tokenCount = new Dictionary<string,int>();
        }

        /// <summary>
        /// Convert the  given text into tokens and then splits it into tokens according to whitespace and punctuation. 
        /// </summary>
        /// <param name="text">Some text</param>
        /// <returns>Lower case tokens</returns>
        public string[] TokeniseString(string text)
        {
            char[] splitters = new char[] { ' ', '\t', '\'', '"', '-', '(', ')', ',', '’', '\n', ':', ';', '?', '.', '!' };
            return text.ToLower().Split(splitters, StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        /// Stems an array of tokens
        /// </summary>
        /// <param name="tokens">An array of lowercase tokens</param>
        /// <returns>An array of stems</returns>
        public string[] StemTokens(string[] tokens){
            int numTokens = tokens.Count();
            string[] stems = new string[numTokens];
            for(int i =0; i< numTokens; i++)
            {
                stems[i] = myStemmer.stemTerm(tokens[i]);
            }
            return stems;
        }
        
        /// <summary>
        /// Counts the occurrences of a given set of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        public void CountOccurrences(string[] tokens)
        {
            foreach (string s in tokens)
            {
                int count=0;
                tokenCount.TryGetValue(s, out count);
                count = count + 1;
                tokenCount[s] = count;
            }
        }
        

        /// <summary>
        /// Removes stopwords from an array of tokens
        /// </summary>
        /// <param name="tokens">An array of tokens</param>
        /// <returns>The array of tokens without any stopwords</returns>
        public string[] StopWordFilter(string[] tokens)
        {

            int numTokens = tokens.Count();
            List<string> filteredTokens = new List<string>();
            for (int i = 0; i < numTokens; i++)
            {
                string token = tokens[i];
                if (!stopWords.Contains(token) && (token.Length > 2)) filteredTokens.Add(token);
            }
            return filteredTokens.ToArray<string>();
        }

        public List<string> FindPhrases(string str)
        {
            List<string> phraseList = new List<string>();
            foreach (Match match in Regex.Matches(str, "\"([^\"]*)\""))
                phraseList.Add(match.ToString());
            return phraseList;
        }
          
        /// <summary>
        /// Counts the occurrences of stems in a given text file ignoring stop words and prints out the results to the screen
        /// </summary>
        /// <param name="str">the information need</param>
        public string[] ProcessText(string str)
        {
            string[] tokens = TokeniseString(str);
            string[] tokensNoStop = StopWordFilter(tokens);
            //string[] stems = StemTokens(tokensNoStop);

            return tokensNoStop;
        }


    }
}
