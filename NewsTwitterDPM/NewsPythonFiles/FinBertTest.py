from transformers import BertTokenizer, BertForSequenceClassification
from transformers import pipeline
import sys
import fileinput
import numpy as np
          
          


def Pipe():
    finbert = BertForSequenceClassification.from_pretrained('yiyanghkust/finbert-tone', num_labels=3)
    tokenizer = BertTokenizer.from_pretrained('yiyanghkust/finbert-tone')
    nlp = pipeline("sentiment-analysis", model=finbert, tokenizer=tokenizer)
    sentences = []
    for line in fileinput.input():
        sentences.append(line)
    
    result = nlp(sentences)
    returnList = []
    for elem in result:
        score = 0
        if elem['label'] == "positive":
            score = 0 + elem['score']
        elif elem['label'] == "negative":
            score = 0 - elem['score']
        elif elem['label'] == "neutral":
            score = 0 + (elem['score'] - 0.5)
        returnList.append(score)
    np.save("/home/martin/RiderProjects/Lexonic/NewsTwitterDPM/SentimentNPYs/CurrentNewsSentiment.npy", np.asarray(returnList))
    return result


if __name__ == '__main__':
    #print("|||||")
    print(Pipe())
    #print("|||||")