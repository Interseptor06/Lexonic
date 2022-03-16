from keras.layers import Concatenate
from keras.layers import Bidirectional
from keras.layers import Dense, Input, Conv2D, MaxPooling2D, LSTM, TimeDistributed, ConvLSTM2D, Flatten, Dropout
from keras.models import Model
import tensorflow as tf

def BuildModel():
    initializer = tf.keras.initializers.RandomNormal()
    MrktNewsFeatureExtractor_Input = Input(shape=(14, 25, 500, 1))
    MrktNewsFeatureExtractor = TimeDistributed(Conv2D(64, kernel_size=(3, 3), activation='sigmoid', kernel_initializer=initializer))(MrktNewsFeatureExtractor_Input)
    MrktNewsFeatureExtractor = TimeDistributed(Conv2D(32, kernel_size=(2, 2), strides=1, activation='sigmoid', kernel_initializer=initializer))(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = TimeDistributed(MaxPooling2D((2, 2), strides=(1, 1)))(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = ConvLSTM2D(96, kernel_size=(5, 5), strides=(2, 2), return_sequences=True, kernel_initializer=initializer, activation='sigmoid')(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = Dropout(0.2)(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = ConvLSTM2D(64, kernel_size=(3, 3), strides=(1, 1), return_sequences=True, kernel_initializer=initializer, activation='sigmoid')(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = Dropout(0.2)(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = ConvLSTM2D(32, kernel_size=(2, 2), strides=(1, 1), kernel_initializer=initializer, activation='sigmoid')(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor = Flatten()(MrktNewsFeatureExtractor)
    MrktNewsFeatureExtractor_Concat = Dense(16, activation='sigmoid', kernel_initializer=initializer)(MrktNewsFeatureExtractor)

    IndexNewsFeatureExtractor_Input = Input(shape=(14, 25, 1))
    IndexNewsFeatureExtractor = TimeDistributed(Bidirectional(LSTM(128, activation='sigmoid', return_sequences=True)))(IndexNewsFeatureExtractor_Input)
    IndexNewsFeatureExtractor = TimeDistributed(Bidirectional(LSTM(256, activation='sigmoid', return_sequences=True)))(IndexNewsFeatureExtractor)
    IndexNewsFeatureExtractor = TimeDistributed(Bidirectional(LSTM(512, activation='sigmoid')))(IndexNewsFeatureExtractor)
    IndexNewsFeatureExtractor = Flatten()(IndexNewsFeatureExtractor)
    IndexNewsFeatureExtractor_Concat = Dense(16, activation='sigmoid', kernel_initializer=initializer)(IndexNewsFeatureExtractor)


    # 100 ->  Number Of Companies, 14 , 5 , 100-> Timesteps, 1 -> Channels
    MrktFeatureExtractor_Input = Input(shape=(14, 5, 500, 1))
    MrktFeatureExtractor = TimeDistributed(Conv2D(128, kernel_size=(3, 3), activation='sigmoid', kernel_initializer=initializer))(MrktFeatureExtractor_Input)
    MrktFeatureExtractor = TimeDistributed(Conv2D(256, kernel_size=(2, 2), strides=1, activation='sigmoid', kernel_initializer=initializer))(MrktFeatureExtractor)
    MrktFeatureExtractor = TimeDistributed(MaxPooling2D((1, 2), strides=(1, 2)))(MrktFeatureExtractor)
    MrktFeatureExtractor = ConvLSTM2D(128, kernel_size=(2, 7), strides=(3, 3), return_sequences=True, kernel_initializer=initializer, activation='sigmoid')(MrktFeatureExtractor)
    MrktFeatureExtractor = Dropout(0.2)(MrktFeatureExtractor)
    MrktFeatureExtractor = ConvLSTM2D(96, kernel_size=(1, 5), strides=(3, 3), return_sequences=True, kernel_initializer=initializer, activation='sigmoid')(MrktFeatureExtractor)
    MrktFeatureExtractor = Dropout(0.2)(MrktFeatureExtractor)
    MrktFeatureExtractor = ConvLSTM2D(64, kernel_size=(1, 3), strides=(3, 3), kernel_initializer=initializer, activation='sigmoid')(MrktFeatureExtractor)
    MrktFeatureExtractor = Flatten()(MrktFeatureExtractor)
    MrktFeatureExtractor_Concat = Dense(16, activation='sigmoid', kernel_initializer=initializer)(MrktFeatureExtractor)

    IndexFeatureExtractor_Input = Input(shape=(14, 5, 1))
    IndexFeatureExtractor = TimeDistributed(Bidirectional(LSTM(128, activation='sigmoid', return_sequences=True, kernel_initializer=initializer)))(IndexFeatureExtractor_Input)
    IndexFeatureExtractor = TimeDistributed(Bidirectional(LSTM(256, activation='sigmoid', return_sequences=True, kernel_initializer=initializer)))(IndexFeatureExtractor)
    IndexFeatureExtractor = TimeDistributed(Bidirectional(LSTM(512, activation='sigmoid', kernel_initializer=initializer)))(IndexFeatureExtractor)
    IndexFeatureExtractor = Flatten()(IndexFeatureExtractor)
    IndexFeatureExtractor_Concat = Dense(16, activation='sigmoid', kernel_initializer=initializer)(IndexFeatureExtractor)

    ModelsConcatenated = Concatenate(axis=-1)([MrktFeatureExtractor_Concat, IndexFeatureExtractor_Concat, MrktNewsFeatureExtractor_Concat, IndexNewsFeatureExtractor_Concat])
    OneMoreDense = Dense(64, activation='sigmoid', kernel_initializer=initializer)(ModelsConcatenated)
    FinalModel_Out = Dense(1, activation='sigmoid')(OneMoreDense)

    Final_Model = Model([MrktFeatureExtractor_Input, IndexFeatureExtractor_Input, MrktNewsFeatureExtractor_Input, IndexNewsFeatureExtractor_Input], FinalModel_Out)
    Final_Model.compile(loss='mse',
                        optimizer='RMSprop',
                        metrics=['mae'])
    return Final_Model


if __name__ == '__main__':
    # x_train, y_train, x_test, y_test = DP.ThreeDimDataPipelineIndex("AAPL", 0.1)
    # x_Four_Dim_Train, x_Four_Dim_Test = DP.FourDimTrainDataPipeline()
    model = BuildModel()
    #model.save("Model.h5")
    print(model.summary())
    # model.fit([x_train, x_Four_Dim_Train], y_train, epochs=10, batch_size=16)
    # print(model.evaluate([[x_test, x_Four_Dim_Test]], y_test))
